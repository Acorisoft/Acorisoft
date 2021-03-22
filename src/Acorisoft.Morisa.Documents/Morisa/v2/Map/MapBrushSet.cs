using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Core;
using LiteDB;

namespace Acorisoft.Morisa.Map
{
    public partial class MapBrushSet : DataSet<MapBrushSetInformation>
    {
        protected internal LiteCollection<IMapBrush> DB_BrushCollection { get; internal set; }
        protected internal LiteCollection<IMapGroup> DB_GroupCollection { get; internal set; }
    }

    #region OldImpl

    public partial class MapBrushSet 
    {
        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Constants
        ////
        ////-------------------------------------------------------------------------------------------------

        //public const string MaintainBrushCollectionName = "Brushes";
        //public const string MaintainGroupCollectionName = "Group";
        //public const string MaintainSettingName = "Setting";
        //public const string MaintainExternalCollectionName = "Externals";

        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Internal Settings
        ////
        ////-------------------------------------------------------------------------------------------------

        //public class Setting
        //{
        //    /// <summary>
        //    /// 获取或设置当前画刷集的名字。
        //    /// </summary>
        //    public string Name { get; set; }

        //    /// <summary>
        //    /// 获取或设置当前画刷集的作者。
        //    /// </summary>
        //    public string Author { get; set; }

        //    /// <summary>
        //    /// 获取或设置当前画刷集的简介。
        //    /// </summary>
        //    public string Summary { get; set; }

        //    /// <summary>
        //    /// 获取或设置当前画刷集的标签。
        //    /// </summary>
        //    public List<string> Tags { get; set; }

        //    /// <summary>
        //    /// 获取或设置当前画刷集的封面。
        //    /// </summary>
        //    public InDatabaseResource Cover { get; set; }
        //}

        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Variables
        ////
        ////-------------------------------------------------------------------------------------------------

        //private readonly LiteDatabase          _DB;
        //private LiteCollection<BsonDocument>   _DB_External;
        //private LiteCollection<IMapBrush>      _DB_Brushes;
        //private LiteCollection<IMapGroup>      _DB_Groups;

        //private bool            _DisposedValue;
        //private Setting         _Setting;
        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Contructors
        ////
        ////-------------------------------------------------------------------------------------------------

        //internal MapBrushSet(LiteDatabase database)
        //{
        //    _DB = database;
        //}
        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Public Methods
        ////
        ////-------------------------------------------------------------------------------------------------

        //public void Dispose()
        //{
        //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}

        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Protected Methods
        ////
        ////-------------------------------------------------------------------------------------------------
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_DisposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: 释放托管状态(托管对象)
        //        }

        //        // TODO: 释放未托管的资源(未托管的对象)并替代终结器
        //        // TODO: 将大型字段设置为 null
        //        _DisposedValue = true;
        //    }
        //}


        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Properties
        ////
        ////-------------------------------------------------------------------------------------------------

        ///// <summary>
        ///// 
        ///// </summary>
        //protected internal LiteDatabase Database => _DB;

        ///// <summary>
        ///// 
        ///// </summary>
        //protected internal LiteCollection<BsonDocument> DB_ExternalCollection
        //{
        //    get => _DB_External;
        //    internal set => _DB_External = value;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //protected internal LiteCollection<IMapBrush> DB_BrushCollection
        //{
        //    get => _DB_Brushes;
        //    internal set => _DB_Brushes = value;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //protected internal LiteCollection<IMapGroup> DB_GroupCollection
        //{
        //    get => _DB_Groups;
        //    internal set => _DB_Groups = value;
        //}

        //protected internal Setting Properties
        //{
        //    get => _Setting;
        //    internal set => _Setting = value;
        //}

        ///// <summary>
        ///// 获取或设置当前画刷集的名字。
        ///// </summary>
        //public string Name
        //{
        //    get => _Setting.Name;
        //}

        ///// <summary>
        ///// 获取或设置当前画刷集的作者。
        ///// </summary>
        //public string Author
        //{
        //    get => _Setting.Author;
        //}

        ///// <summary>
        ///// 获取或设置当前画刷集的简介。
        ///// </summary>
        //public string Summary
        //{
        //    get => _Setting.Summary;
        //}

        ///// <summary>
        ///// 获取或设置当前画刷集的标签。
        ///// </summary>
        //public List<string> Tags
        //{
        //    get => _Setting.Tags;
        //}

        ///// <summary>
        ///// 获取或设置当前画刷集的封面。
        ///// </summary>
        //public InDatabaseResource Cover
        //{
        //    get => _Setting.Cover;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public IGroupCollection BrushGroups { get; internal set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public IMapBrushCollection Brushes { get; internal set; }

        ////-------------------------------------------------------------------------------------------------
        ////
        ////  Finalizer
        ////
        ////-------------------------------------------------------------------------------------------------

        //// TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        //~MapBrushSet()
        //{
        //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //    Dispose(disposing: false);
        //}

    }

    #endregion OldImpl
}
