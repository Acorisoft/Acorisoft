using System;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Services;
using ReactiveUI;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public abstract class AppViewModelBase : ViewModelBase, IScreen, IAppViewModel
    {
        private readonly CompositeDisposable _diposable;
        private readonly Subject<string> _titleStream;
        
        private readonly ObservableAsPropertyHelper<IPageViewModel> _current;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _quickView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _toolView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _extraView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _contextView;
        private readonly ObservableAsPropertyHelper<string> _title;

        protected AppViewModelBase(IViewService viewService)
        {
            _diposable = new CompositeDisposable();
            _current = viewService.Page.ToProperty(this, nameof(CurrentViewModel));
            _quickView = viewService.QuickView.ToProperty(this, nameof(QuickView)).DisposeWith(_diposable);
            _toolView = viewService.ToolView.ToProperty(this, nameof(ToolView)).DisposeWith(_diposable);
            _extraView = viewService.ExtraView.ToProperty(this, nameof(ExtraView)).DisposeWith(_diposable);
            _contextView = viewService.ContextView.ToProperty(this, nameof(ContextView)).DisposeWith(_diposable);
            _titleStream = new Subject<string>().DisposeWith(_diposable);
            _title = _titleStream.ToProperty(this, nameof(Title));
            viewService.Page.Subscribe(SubscribePageChanged).DisposeWith(_diposable);
            viewService.QuickView.Subscribe(SubscribeQuickViewChanged).DisposeWith(_diposable);
            viewService.ContextView.Subscribe(SubscribeQuickViewChanged).DisposeWith(_diposable);
            viewService.ToolView.Subscribe(SubscribeQuickViewChanged).DisposeWith(_diposable);
            viewService.ExtraView.Subscribe(SubscribeQuickViewChanged).DisposeWith(_diposable);
            Router = new RoutingState();
        }

        private void SubscribeQuickViewChanged(IQuickViewModel page)
        {
            if (page is null)
            {
                return;
            }

            page.Start(CurrentViewModel);
        }
        private void SubscribePageChanged(IPageViewModel page)
        {
            if (page is null)
            {
                return;
            }
            
            _titleStream.OnNext(page.Title);
            Router.Navigate.Execute((PageViewModelBase) page);
        }

        public IPageViewModel CurrentViewModel => _current.Value;
        public string Title => _title.Value;
        public IQuickViewModel QuickView => _quickView.Value;
        public IQuickViewModel ToolView => _toolView.Value;
        public IQuickViewModel ContextView => _contextView.Value;
        public IQuickViewModel ExtraView => _extraView.Value;
        public RoutingState Router { get; }
    }
}