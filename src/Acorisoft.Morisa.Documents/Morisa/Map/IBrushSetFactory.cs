using DynamicData;
using SixLabors.ImageSharp.PixelFormats;
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
        /// <param name="newGroup"></param>
        public void Add(IBrushGroup newGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroup"></param>
        /// <param name="parentGroup"></param>
        public void Add(IBrushGroup newGroup, IBrushGroup parentGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroups"></param>
        public void Add(IEnumerable<IBrushGroup> newGroups);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroups"></param>
        /// <param name="parentGroup"></param>
        public void Add(IEnumerable<IBrushGroup> newGroups, IBrushGroup parentGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="parentGroup"></param>
        /// <param name="landColor">陆地颜色</param>
        public void Add(IGenerateContext<Brush> brush, IBrushGroup parentGroup, Rgba32 landColor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brushes"></param>
        /// <param name="parentGroup"></param>
        public void Add(IEnumerable<IGenerateContext<Brush>> brushes, IBrushGroup parentGroup, Rgba32 landColor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public bool Remove(IBrush brush);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brushes"></param>
        /// <returns></returns>
        public bool Remove(IEnumerable<IBrush> brushes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public void Remove(IBrushGroup group);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public void Remove(IEnumerable<IBrushGroup> groups);

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllBrushes();

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllGroups();

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IComparer<IBrush>> SorterStream { get; }

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IPageRequest> PageStream { get; }
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<BrushAdapter> Brushes { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<BrushGroupAdapter> Groups { get; }
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
