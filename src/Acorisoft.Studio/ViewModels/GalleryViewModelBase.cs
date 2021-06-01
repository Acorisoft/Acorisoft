using System.Collections.ObjectModel;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;

namespace Acorisoft.Studio.ViewModels
{
    public class GalleryViewModelBase<TEngine, TIndex, TWrapper, TComposition> : PageViewModelBase
        where TEngine : ComposeSetSystemModule<TIndex, TWrapper, TComposition>
        where TIndex : DocumentIndex
        where TWrapper : DocumentIndexWrapper<TIndex>
        where TComposition : Document
    {
        protected GalleryViewModelBase(IComposeSetSystem system, TEngine engine)
        {
            Engine = engine;
            System = system;
        }
        
        protected IComposeSetSystem System { get; }
        protected TEngine Engine { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<TWrapper> Collection => Engine.Collection;
    }
}