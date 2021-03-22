using Acorisoft.Morisa.Core;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public interface IMapBrushSetFactory : IDataSetManager<MapBrushSet, MapBrushSetInformation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Load(IStoreContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Generate(IGenerateContext<MapBrushSetInformation> context);

        /// <summary>
        /// 
        /// </summary>
        SourceList<IMapGroup> MapGroupSource { get; }

        /// <summary>
        /// 
        /// </summary>
        SourceList<IMapBrush> MapBrushSource { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<IMapGroupAdapter> GroupCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<IMapBrush> BrushCollection { get; }

        /// <summary>
        /// 用于指示视图模型当前已经加载了数据，通知视图模型以重建模型。
        /// </summary>
        event EventHandler OnLoaded;
    }
}
