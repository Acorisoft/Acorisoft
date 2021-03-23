using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBrushSetFactory : IDataSetFactory<BrushSet, BrushSetProperty>
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<IBrush> Brushes { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<IBrushGroupTree> Groups { get; }
    }
}
