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

    //
    // this is a special data set factory,because we should considering about the multiple data set scenarios
    // the first scenario is that user use it to add and edit
    // the second and the lastest scenario is user use it to load brush
    //
    // * IBrushSetFactory
    //  * Load
    //  * Generate
    //  *

    // * IBrushSetFactory
}
