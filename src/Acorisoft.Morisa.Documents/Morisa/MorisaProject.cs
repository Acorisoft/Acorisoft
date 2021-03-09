using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BsonDocumentCollection = LiteDB.ILiteCollection<LiteDB.BsonDocument>;
namespace Acorisoft.Morisa
{
#pragma warning disable CA1816
    public class MorisaProject : IMorisaProject, IDisposable
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------

        public const string SettingObjectName = "MorisaProject.Setting";
        public const string BinariesCollectionName = "Binaries";

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

            [BsonRef(BinariesCollectionName)]
            public ImageObject Cover { get; set; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly ILiteDatabase              _Database;
        private readonly BsonDocumentCollection     _Externals;
        private readonly bool                       _IsNeedInitialize;
        private readonly Setting                    _Setting;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        internal MorisaProject(ILiteDatabase db)
        {
            _Database = db ?? throw new ArgumentNullException(nameof(db));
            _IsNeedInitialize = !Database.CollectionExists(MorisaProjectManager.ExternalCollectionName);
            _Externals = _Database.GetCollection(MorisaProjectManager.ExternalCollectionName);
            if (_IsNeedInitialize)
            {
                _Setting = new Setting
                {
                    Name = "Test" ,
                    Summary = "Summary" ,
                    Topic = "Topic"
                };
                UpdateSetting();
            }
            else
            {
                _Setting = _Externals.FindOne<Setting>(SettingObjectName);
            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Private Methods
        //
        //-------------------------------------------------------------------------------------------------

        void UpdateSetting()
        {
            //
            var document = BsonMapper.Global.Serialize(_Setting).AsDocument;

            //
            _Externals.Upsert(SettingObjectName , document);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            _Database.Dispose();
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        protected internal ILiteDatabase Database => _Database;

        /// <summary>
        /// 
        /// </summary>
        public bool IsNeedInitialize => _IsNeedInitialize;

        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get => _Setting.Name;
            set {
                _Setting.Name = value;
                UpdateSetting();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Summary {
            get => _Setting.Summary;
            set {
                _Setting.Summary = value;
                UpdateSetting();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Topic {
            get => _Setting.Topic;
            set {
                _Setting.Topic = value;
                UpdateSetting();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ImageObject Cover {
            get => _Setting.Cover;
            set {
                _Setting.Cover = value;
                UpdateSetting();
            }
        }
    }
}
