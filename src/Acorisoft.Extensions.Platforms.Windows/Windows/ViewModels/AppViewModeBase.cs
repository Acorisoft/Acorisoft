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
        private readonly Subject<string> _titleStream;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _quickView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _toolView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _extraView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _contextView;
        private readonly ObservableAsPropertyHelper<string> _title;

        protected AppViewModelBase(IViewService viewService)
        {
            _quickView = viewService.QuickView.ToProperty(this, nameof(QuickView));
            _toolView = viewService.ToolView.ToProperty(this, nameof(ToolView));
            _extraView = viewService.ExtraView.ToProperty(this, nameof(ExtraView));
            _contextView = viewService.ContextView.ToProperty(this, nameof(ContextView));
            _titleStream = new Subject<string>();
            _title = _titleStream.ToProperty(this, nameof(Title));
            viewService.Page.Subscribe(SubscribePageChanged);
            Router = new RoutingState();
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

        public string Title => _title.Value;
        public IQuickViewModel QuickView => _quickView.Value;
        public IQuickViewModel ToolView => _toolView.Value;
        public IQuickViewModel ContextView => _contextView.Value;
        public IQuickViewModel ExtraView => _extraView.Value;
        public RoutingState Router { get; }
    }
}