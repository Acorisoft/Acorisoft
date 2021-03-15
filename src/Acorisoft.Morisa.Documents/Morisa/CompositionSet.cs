using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class CompositionSet : ICompositionSet, INotifyPropertyChanged
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------
        public const string ImageDirectoryName = "Images";
        public const string VideoDirectoryName = "Videos";
        public const string MainDatabaseName = "Main.Morisa-Project";
        public const string MainDatabaseSettingName = "Morisa.Setting";
        public const string ExternalCollectionName = "Externals";
        public const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        protected class Setting
        {
            public string Name { get; set; }
            public string Summary { get; set; }
            public string Topic { get; set; }
            public List<string> Tags { get; set; }
            public Resource Cover { get; set; }
        }

        //
        // 1)
        // When we open an string that represents a directory,we will open the whole directory
        // and find out the main database.
        //
        // 2)
        // When we open a string that represents a file,we will open the main database ,and get
        // all the file in folder.
        //
        // 3)
        // CompositionSet represents a whole directory,main database just a part of this,image files
        // video files and large size files will set into the directory.
        //
        // 4)
        // The Directory structure like following hierachy:
        //
        // ...\DirName
        // ...\DirName\Images
        // ...\DirName\Videos
        // ...\DirName\
        //
        // 5)
        // When we open an ICompositionSetInfo that represents a creation context information.
        // that we use it to  generate an new ICompositionSet
        //
        // 6)
        // When we open an ICompositionSetStore that represents a recent file open record, that
        // we will use it to open an new ICompositionSet File

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        [BsonIgnore]
        [NonSerialized]
        private Setting _Setting;

        [BsonIgnore]
        [NonSerialized]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangedEventHandler ChangedHandler;

        [BsonIgnore]
        [NonSerialized]
        private bool _DisposedValue;

        private LiteCollection<BsonDocument> _DB_Externals;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public CompositionSet(string directory, string file)
        {
            Directory = directory;
            Database = new LiteDatabase(new ConnectionString
            {
                Filename = file,
                Mode = LiteDB.FileMode.Shared,

            });
            OnInitialize();
        }

        public CompositionSet(string directory, string file, ICompositionSetInfo css)
        {
            Directory = directory;
            Database = new LiteDatabase(new ConnectionString
            {
                Filename = file,
                Mode = LiteDB.FileMode.Shared,

            });
            OnInitialize(css);
        }

        internal CompositionSet(string directory)
        {
            Directory = directory;
            Database = new LiteDatabase(new ConnectionString
            {
                Filename = Path.Combine(directory, MainDatabaseName),
                Mode = LiteDB.FileMode.Shared,

            });
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Methods
        //
        //-------------------------------------------------------------------------------------------------

        protected virtual void OnInitialize()
        {
            if (Database.CollectionExists(MainDatabaseSettingName))
            {
                _DB_Externals = Database.GetCollection(ExternalCollectionName);
                _DB_Externals.FindById(MainDatabaseSettingName)
                             .Deserialize<Setting>();
            }
            else
            {
                _DB_Externals = Database.GetCollection(ExternalCollectionName);
                _Setting = CreateInstanceCore();
                _DB_Externals.Upsert(MainDatabaseSettingName, DatabaseMixins.Serialize(_Setting));
            }

            RaiseUpdate(nameof(Name));
            RaiseUpdate(nameof(Summary));
            RaiseUpdate(nameof(Topic));
            RaiseUpdate(nameof(Tags));
            RaiseUpdate(nameof(Cover));
        }

        public void Update()
        {
            _DB_Externals.Upsert(MainDatabaseSettingName, DatabaseMixins.Serialize(_Setting));
        }

        protected virtual void OnInitialize(ICompositionSetInfo css)
        {
            _Setting = new Setting
            {
                Name = css.Name,
                Cover = css.Cover,
                Summary = css.Summary,
                Tags = css.Tags,
                Topic = css.Topic
            };
            Database.GetCollection(ExternalCollectionName)
                    .Upsert(MainDatabaseSettingName, DatabaseMixins.Serialize(_Setting));

            RaiseUpdate(nameof(Name));
            RaiseUpdate(nameof(Summary));
            RaiseUpdate(nameof(Topic));
            RaiseUpdate(nameof(Tags));
            RaiseUpdate(nameof(Cover));
        }

        protected virtual Setting CreateInstanceCore()
        {
            return new Setting
            {
                Name = "Untitled",
                Summary = LoremIpsum,
                Topic = LoremIpsum,
                Cover = null,
                Tags = new List<string> { "test", "set" }
            };
        }

        protected void RaiseUpdate([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    Database.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                _DisposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 获取当前设定集所在的目录。
        /// </summary>
        protected internal string Directory
        {
            get;
        }

        /// <summary>
        /// 获取当前设定集的主数据库。
        /// </summary>
        protected internal LiteDatabase Database
        {
            get;
        }

        /// <summary>
        /// 获取当前设定集所关联的图片目录。
        /// </summary>
        protected internal string ImageDirectory
        {
            get
            {
                return Path.Combine(Directory, ImageDirectoryName);
            }
        }

        /// <summary>
        /// 获取当前设定集所关联的视频目录。
        /// </summary>
        protected internal string VideoDirectory
        {
            get
            {
                return Path.Combine(Directory, VideoDirectoryName);
            }
        }

        public string Name
        {
            get => _Setting.Name;
            set
            {
                _Setting.Name = value;
                RaiseUpdate(nameof(Name));
            }
        }

        public string Summary
        {
            get => _Setting.Name;
            set
            {
                _Setting.Name = value;
                RaiseUpdate(nameof(Summary));
            }
        }
        public string Topic
        {
            get => _Setting.Name;
            set
            {
                _Setting.Name = value;
                RaiseUpdate(nameof(Topic));
            }
        }

        public List<string> Tags
        {
            get => _Setting.Tags;
            set
            {
                _Setting.Tags = value;
                RaiseUpdate(nameof(Tags));
            }
        }

        public Resource Cover
        {
            get => _Setting.Cover;
            set
            {
                _Setting.Cover = value;
                RaiseUpdate(nameof(Cover));
            }
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Events
        //
        //-------------------------------------------------------------------------------------------------

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => ChangedHandler += value;
            remove => ChangedHandler -= value;
        }


        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~CompositionSet()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }
    }
}
