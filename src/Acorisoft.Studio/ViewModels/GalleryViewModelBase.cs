using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.Systems;
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
        private protected readonly ObservableAsPropertyHelper<int> PageCountProperty;
        private protected readonly ObservableAsPropertyHelper<bool> IsEmptyProperty;
        private IComparer<TWrapper> _sorter;

        protected GalleryViewModelBase(IComposeSetSystem system, TEngine engine)
        {
            Engine = engine;
            System = system;

            //
            // Create Property
            IsEmptyProperty = Engine.IsEmpty.ToProperty(this, nameof(IsEmpty));
            CountProperty = Engine.Count.ToProperty(this, nameof(Count));
            PageCountProperty = Engine.PageCount.ToProperty(this, nameof(PageCount));
            
            //
            // Create Command
            RefreshCommand = ReactiveCommand.Create(OnRefresh, System.IsOpen);
            DeleteThisCommand = ReactiveCommand.Create<TWrapper>(OnDeleteThis, System.IsOpen);
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
        
        protected async void OnDeleteThis(TWrapper wrapper)
        {
            await Engine.DeleteThisAsync(wrapper.Source);
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
                PageItemCount = result;
            }
        }

        /// <summary>
        /// 刷新命令
        /// </summary>

        protected IComposeSetSystem System { get; }

        /// <summary>
        /// 刷新命令
        /// </summary>
        protected TEngine Engine { get; }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand PerPageItemCountOptionCommand { get; }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand DeleteThisCommand { get; }
        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand DeleteThisPageCommand { get; }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand DeleteAllCommand { get; }
        
        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand RefreshCommand { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<TWrapper> Collection => Engine.Collection;

        /// <summary>
        /// 排序工具
        /// </summary>
        public IComparer<TWrapper> Sorter
        {
            get => _sorter;
            set
            {
                if (Set(ref _sorter, value))
                {
                    Engine.Sorter = value;
                }
            }
        }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public int PageItemCount
        {
            get => Engine.PerPageItemCount;
            set
            {
                Engine.PerPageItemCount = value;
                RaiseUpdated();
            }
        }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public int PageIndex
        {
            get => Engine.PageIndex;
            set
            {
                Engine.PageIndex = value;
                RaiseUpdated();
            }
        }
        
        /// <summary>
        /// 页面数量
        /// </summary>
        public int PageCount => PageCountProperty.Value;
        
        /// <summary>
        /// 刷新命令
        /// </summary>
        public int Count => CountProperty.Value;

        /// <summary>
        /// 刷新命令
        /// </summary>
        public bool IsEmpty => IsEmptyProperty.Value;
    }
}