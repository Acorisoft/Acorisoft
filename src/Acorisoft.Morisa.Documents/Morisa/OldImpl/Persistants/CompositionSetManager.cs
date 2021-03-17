using Acorisoft.Morisa.Emotions;
using LiteDB;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Properties;
using Acorisoft.Morisa.Core;

namespace Acorisoft.Morisa.Persistants
{
    [Obsolete]
    public partial class CompositionSetManager : ICompositionSetManager
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        private ICompositionSet _CurrentCompositionSet;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructor
        //
        //-------------------------------------------------------------------------------------------------
        public CompositionSetManager()
        {
        }
        //-------------------------------------------------------------------------------------------------
        //
        //  Private Methods
        //
        //-------------------------------------------------------------------------------------------------
        void LoadImpl(string target)
        {
            if (Directory.Exists(target))
            {
                LoadAsDirectory(target);
            }
            else
            {
                if (File.Exists(target))
                {
                    LoadAsFile(target);
                }
                else
                {
                    throw new InvalidOperationException(string.Concat(target, SR.CannotLoadAnEmptyFileOrDirectory));
                }
            }
        }

        void LoadAsFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new InvalidOperationException(string.Concat(file, SR.FileNotFound));
            }

            var directory = Directory.GetParent(file).FullName;
            LoadCore(directory, file);
        }

        void LoadAsDirectory(string directory)
        {
            var file = Path.Combine(directory,CompositionSet.MainDatabaseName);

            if (File.Exists(file))
            {
                LoadCore(directory, file);
            }
            else
            {
                throw new InvalidOperationException(string.Concat(file, SR.FileNotFound));
            }
        }

        void LoadCore(string directory, string file)
        {
            var cs = new CompositionSet(directory,file);
            var css = new CompositionSetStore
            {
                Directory = directory,
                FileName = file,
                Name = cs.Name
            };
            Changed?.Invoke(this, new CompositionSetChangedEventArgs(_CurrentCompositionSet, cs));
            Opened?.Invoke(this, new CompositionSetOpenedEventArgs(css));

            if (_CurrentCompositionSet != null)
            {
                _CurrentCompositionSet.Dispose();
                _CurrentCompositionSet = null;
            }
            _CurrentCompositionSet = cs;
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------
        /// <summary>
        /// 加载一个指定的文件夹或者文件
        /// </summary>
        /// <param name="target">指定要打开的文件或者文件夹。</param>
        public void Load(string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new InvalidOperationException(string.Concat(target, SR.CannotLoadAnEmptyFileOrDirectory));
            }

            LoadImpl(target);
        }

        public void Load(ICompositionSetInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.Directory) || string.IsNullOrEmpty(info.FileName))
            {
                throw new InvalidOperationException(string.Concat(info.Directory, SR.CannotLoadAnEmptyFileOrDirectory));
            }

            //if (!File.Exists(info.FileName))
            //{
            //    throw new InvalidOperationException(string.Concat(info.FileName, SR.FileNotFound));
            //}

            if (!Directory.Exists(info.Directory))
            {
                var parentDir = Directory.GetParent(info.Directory).FullName;
                if (!Directory.Exists(parentDir))
                {
                    throw new InvalidOperationException(string.Concat(info.Directory, SR.CannotLoadAnEmptyFileOrDirectory));
                }
                else
                {
                    Directory.CreateDirectory(info.Directory);
                }
            }

            var css = new CompositionSetStore
            {
                Directory = info.Directory,
                FileName = info.FileName,
                Name = info.Name
            };

            var cs = new CompositionSet(info.Directory, info.FileName, info);

            //
            // TODO: 处理设定集封面
            if (cs.Cover != null)
            {
                if (cs.Cover is InDatabaseResource idr && !string.IsNullOrEmpty(idr.FileName) && File.Exists(idr.FileName))
                {
                    if (string.IsNullOrEmpty(idr.Id))
                    {
                        idr.Id = CompositionElementFactory.GenereateGuid();
                    }
                    cs.Database.FileStorage.Upload(idr.Id, idr.FileName);
                }
                else if(cs.Cover is OutsideResource osr)
                {
                    if (string.IsNullOrEmpty(osr.Id))
                    {
                        osr.Id = CompositionElementFactory.GenereateGuid();
                    }

                    //
                    // Copy To Images Folder
                    // That image Rename
                    var extension = new FileInfo(osr.FileName).Extension;
                    File.Copy(osr.FileName, Path.Combine(cs.ImageDirectory, osr.Id + extension));
                }
            }

            Changed?.Invoke(this, new CompositionSetChangedEventArgs(_CurrentCompositionSet, cs));
            Opened?.Invoke(this, new CompositionSetOpenedEventArgs(css));

            if (_CurrentCompositionSet != null)
            {
                _CurrentCompositionSet.Dispose();
                _CurrentCompositionSet = null;
            }

            _CurrentCompositionSet = cs;
        }

        public void Load(ICompositionSetStore store)
        {
            if (store == null || string.IsNullOrEmpty(store.Directory) || string.IsNullOrEmpty(store.FileName))
            {
                throw new InvalidOperationException(string.Concat(store.Directory, SR.CannotLoadAnEmptyFileOrDirectory));
            }

            if (!File.Exists(store.FileName))
            {
                throw new InvalidOperationException(string.Concat(store.FileName, SR.FileNotFound));
            }

            if (!Directory.Exists(store.Directory))
            {
                var parentDir = Directory.GetParent(store.Directory).FullName;
                if (!Directory.Exists(parentDir))
                {
                    throw new InvalidOperationException(string.Concat(store.Directory, SR.CannotLoadAnEmptyFileOrDirectory));
                }
                else
                {
                    Directory.CreateDirectory(store.Directory);
                }
            }

            var cs = new CompositionSet(store.Directory,store.FileName);
            Changed?.Invoke(this, new CompositionSetChangedEventArgs(_CurrentCompositionSet, cs));

            if (_CurrentCompositionSet != null)
            {
                _CurrentCompositionSet.Dispose();
                _CurrentCompositionSet = null;
            }

            _CurrentCompositionSet = cs;
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        [BsonIgnore]
        public event EventHandler<CompositionSetChangedEventArgs> Changed;


        [BsonIgnore]
        public event EventHandler<CompositionSetOpenedEventArgs> Opened;
    }
}
