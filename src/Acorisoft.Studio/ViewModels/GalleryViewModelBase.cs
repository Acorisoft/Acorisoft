using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class GalleryViewModelBase<TEngine, TIndex, TWrapper, TComposition> : PageViewModelBase
        where TEngine : ComposeSetSystemModule<TIndex, TWrapper, TComposition>
        where TIndex : DocumentIndex
        where TWrapper : DocumentIndexWrapper<TIndex>
        where TComposition : Document
    {
        private protected readonly ObservableAsPropertyHelper<int> CountProperty;
        protected GalleryViewModelBase(IComposeSetSystem system, TEngine engine)
        {
            Engine = engine;
            System = system;
            CountProperty = Engine.Count.ToProperty(this, nameof(Count));
            RefreshCommand = ReactiveCommand.Create(OnRefresh, System.IsOpen);
            DeleteThisPageCommand = ReactiveCommand.Create(OnDeleteThisPage, System.IsOpen);
            DeleteAllCommand = ReactiveCommand.Create(OnDeleteAll, System.IsOpen);
            PerPageItemCountOptionCommand = ReactiveCommand.Create(OnChoosePerPageItemCountOption, System.IsOpen);
        }

        protected async void OnRefresh()
        {
            await Engine.RefershAsync();
        }

        protected async void OnDeleteAll()
        {
            await Engine.DeleteAllAsync();
        }
        
        protected async void OnDeleteThisPage()
        {
            await Engine.DeleteThisPageAsync();
        }
        
        protected async void OnChoosePerPageItemCountOption()
        {
            var session = await ShowDialog<PageItemCountViewModel>();
            if (session.IsCompleted && session.GetResult<int>() is var result)
            {
                Engine.PerPageItemCount = result;
            }
        }
        
        protected IComposeSetSystem System { get; }
        protected TEngine Engine { get; }
        
        public ICommand PerPageItemCountOptionCommand { get; }
        public ICommand DeleteThisPageCommand { get; }
        public ICommand DeleteAllCommand { get; }
        
        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand RefreshCommand { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<TWrapper> Collection => Engine.Collection;

        public int Count => CountProperty.Value;
    }
}