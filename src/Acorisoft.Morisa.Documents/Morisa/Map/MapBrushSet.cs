using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Core;
using LiteDB;

namespace Acorisoft.Morisa.Map
{
    public class MapBrushSet : IDisposable
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------

        public const string MaintainCoverCollectionName = "Covers";
        public const string MaintainBrushCollectionName = "Brushes";
        public const string MaintainGroupCollectionName = "Group";
        public const string MaintainSettingName = "Setting";
        public const string MaintainExternalCollectionName = "Externals";

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Settings
        //
        //-------------------------------------------------------------------------------------------------

        protected class Setting
        {
            /// <summary>
            /// 获取或设置当前画刷集的名字。
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 获取或设置当前画刷集的作者。
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// 获取或设置当前画刷集的简介。
            /// </summary>
            public string Summary { get; set; }

            /// <summary>
            /// 获取或设置当前画刷集的标签。
            /// </summary>
            public List<string> Tags { get; set; }

            /// <summary>
            /// 获取或设置当前画刷集的封面。
            /// </summary>
            public InDatabaseResource Cover { get; }
        }

        protected class ResourceUsage
        {
            [BsonId]
            public string Id { get; set; }
            public InDatabaseResource Resource { get; set; }
            public bool Usage { get; set; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly LiteDatabase                   _DB;
        private readonly LiteCollection<BsonDocument>   _DB_External;
        private readonly LiteCollection<IMapBrush>      _DB_Brushes;
        private readonly LiteCollection<IMapGroup>      _DB_Groups;
        private readonly LiteCollection<ResourceUsage>  _DB_Covers;

        private bool            _DisposedValue;
        private Setting         _Setting;
        //-------------------------------------------------------------------------------------------------
        //
        //  Contructors
        //
        //-------------------------------------------------------------------------------------------------
        public MapBrushSet(string fileName, string name, string summary, string author, IEnumerable<string> tags, InDatabaseResource cover)
        {

        }

        public MapBrushSet(LiteDatabase db, string name, string summary, string author, IEnumerable<string> tags, InDatabaseResource cover)
        {

        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------
        public void UpdateSetting()
        {
            //
            // Update
            _DB_External.Upsert(BsonMapper.Global.ToDocument(_Setting));
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Protected Methods
        //
        //-------------------------------------------------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                _DisposedValue = true;
            }
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------


        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~MapBrushSet()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

    }
}
