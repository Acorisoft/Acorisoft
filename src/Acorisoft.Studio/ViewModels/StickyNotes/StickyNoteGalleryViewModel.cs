using System;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.ProjectSystems;
using Acorisoft.Studio.Properties;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class StickyNoteGalleryViewModel : PageViewModelBase, IGalleryViewModel<StickyNoteIndex>
    {
        private readonly CompositeDisposable _disposable;
        private readonly StickyNoteEngine _engine;
        private readonly IComposeSetSystem _css;

        private readonly ObservableAsPropertyHelper<int> _countProperty;

        public StickyNoteGalleryViewModel(IComposeSetSystem css, StickyNoteEngine engine)
        {
            _disposable = new CompositeDisposable();
            _css = css;
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _countProperty = _engine.Count.ToProperty(this, nameof(Count));

            //
            // 按创建时间排序 按修改时间排序 
            NewCommand = ReactiveCommand.Create(OnNewItem, _engine.IsOpen);
            DeleteThisCommand = ReactiveCommand.Create<StickyNoteIndexWrapper>(OnDeleteThis, _engine.IsOpen);
            DeleteAllCommand = ReactiveCommand.Create(OnDeleteAll, _engine.IsOpen);
            DeleteThisPageCommand = ReactiveCommand.Create(OnDeleteThisPage, _engine.IsOpen);
            OpenThisCommand = ReactiveCommand.Create<StickyNoteIndexWrapper>(OnOpenItem, _engine.IsOpen);
        }

        public async Task SearchAsync(string keyword)
        {
            await _engine.FindAsync(keyword);
        }

        protected override async void OnStart()
        {
            // using (ViewAware.ForceBusyState("打开项目"))
            // {
            //     var compositionSetManager = ServiceLocator.CompositionSetManager;
            //     try
            //     {
            //         await compositionSetManager.LoadProject(compositionSetManager.CompositionSets.FirstOrDefault());
            //     }
            //     catch
            //     {
            //         ViewAware.Toast("打开失败");
            //     }
            // }
        }

        protected override void OnStop()
        {
            _disposable.Dispose();
        }

        protected async void OnNewItem()
        {
            var newInfo = new NewItemInfo<StickyNoteDocument>(new StickyNoteDocument())
            {
                Name = SR.StickyNoteEngine_EmptyDocumentName
            };

            //
            // 等待创建
            await _engine.NewAsync(newInfo);

            //
            // 跳转
        }

        protected async void OnDeleteThisPage()
        {
            await _engine.DeleteThisPageAsync();
        }

        protected async void OnDeleteThis(StickyNoteIndexWrapper item)
        {
            await _engine.DeleteThisAsync(item.Source);

            //
            // 跳转
        }

        protected async void OnDeleteAll()
        {
            //
            // 等待创建
            await _engine.DeleteAllAsync();

            //
            // 跳转
        }

        protected async void OnOpenItem(StickyNoteIndexWrapper item)
        {
            //
            // 打开文档
            var document = await _engine.OpenAsync(item.Source);

            //
            // 跳转
            ViewAware.NavigateTo<StickyNoteViewModel>(document);
        }

        public int Count => _countProperty.Value;

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
        public ReadOnlyObservableCollection<StickyNoteIndexWrapper> Collection => _engine.Collection;

        /// <summary>
        /// 获取或设置当前的
        /// </summary>
        public StickyNoteDocument Document { get; }
    }
}