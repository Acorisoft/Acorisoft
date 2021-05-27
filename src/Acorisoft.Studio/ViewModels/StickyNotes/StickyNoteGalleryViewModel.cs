using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.StickyNote;
using Acorisoft.Studio.Engines;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class StickyNoteGalleryViewModel : PageViewModelBase, IGalleryViewModel<StickyNoteIndex>
    {
        private readonly CompositeDisposable _disposable;
        private readonly StickyNoteEngine _engine;
        
        public StickyNoteGalleryViewModel(StickyNoteEngine engine)
        {
            _disposable = new CompositeDisposable();
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            
            OpenThisCommand = ReactiveCommand.Create<StickyNoteIndex>(
                OpenStickyNote,
                ServiceLocator.CompositionSetManager.IsOpen)
                .DisposeWith(_disposable);
        }

        private async void OpenStickyNote(StickyNoteIndex index)
        {
            if (index == null)
            {
                return;
            }

            if (index.Id == Guid.Empty)
            {

            }

            //
            // 打开文档。
            try
            {
                using (ViewAware.ForceBusyState("正在打开标签"))
                {
                    //
                    // 获取文档
                    var document = await _engine.Open(index);
                    
                    //
                    // 跳转
                    ViewAware.NavigateTo<StickyNoteViewModel>(new Hashtable
                    {
                        { "arg1", document }
                    });
                }
            }
            catch(Exception ex)
            {

            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ISearchViewModel<StickyNoteIndex> Search { get; }
        
        /// <summary>
        /// 新建便签命令
        /// </summary>
        public ICommand NewCommand { get; }

        /// <summary>
        /// 打开便签命令
        /// </summary>
        public ICommand OpenThisCommand { get; }

        /// <summary>
        /// 删除当前便签命令
        /// </summary>
        public ICommand DeleteThisCommand { get; }

        /// <summary>
        /// 删除当前页命令
        /// </summary>
        public ICommand DeleteThisPageCommand { get; }

        /// <summary>
        /// 删除所有命令
        /// </summary>
        public ICommand DeleteAllCommand { get; }

        /// <summary>
        /// 保存命令
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// 第一页命令
        /// </summary>
        public ICommand FirstPageCommand { get; }

        /// <summary>
        /// 最后一页命令
        /// </summary>
        public ICommand LastPageCommand { get; }

        /// <summary>
        /// 上一页命令
        /// </summary>
        public ICommand PreviousPageCommand { get; }

        /// <summary>
        /// 下一页命令
        /// </summary>
        public ICommand NextPageCommand { get; }

        /// <summary>
        /// 跳转到指定页面命令
        /// </summary>
        public ICommand GotoPageCommand { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<StickyNoteIndex> Collection { get; }
        
        /// <summary>
        /// 获取或设置当前的
        /// </summary>
        public StickyNoteDocument Document { get; }
    }
}