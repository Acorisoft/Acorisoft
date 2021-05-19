using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Windows.Platforms;
using ReactiveUI;
using Splat;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class AppViewModelBase : ViewModelBase, IScreen, IAppViewModel
    {
        private readonly CompositeDisposable _disposable;
        private readonly Subject<IPageViewModel> _currentViewModelStream;
        private readonly Subject<IQuickViewModel> _quickViewStream;
        private readonly Subject<IQuickViewModel> _toolViewStream;
        private readonly Subject<IQuickViewModel> _contextualViewStream;
        private readonly Subject<IQuickViewModel> _extraViewStream;
        private readonly ObservableAsPropertyHelper<IPageViewModel> _currentViewModel;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _quickView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _toolView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _extraView;
        private readonly ObservableAsPropertyHelper<IQuickViewModel> _contextualView;

        protected AppViewModelBase()
        {
            _disposable = new CompositeDisposable();
            _currentViewModelStream = new Subject<IPageViewModel>();
            _quickViewStream = new Subject<IQuickViewModel>();
            _toolViewStream = new Subject<IQuickViewModel>();
            _contextualViewStream = new Subject<IQuickViewModel>();
            _extraViewStream = new Subject<IQuickViewModel>();

            
            _currentViewModel = _currentViewModelStream.ToProperty(this, nameof(Current)).DisposeWith(_disposable);
            _quickView = _quickViewStream.ToProperty(this, nameof(QuickView)).DisposeWith(_disposable);
            _toolView = _toolViewStream.ToProperty(this, nameof(ToolView)).DisposeWith(_disposable);
            _extraView = _contextualViewStream.ToProperty(this, nameof(ExtraView)).DisposeWith(_disposable);
            _contextualView = _extraViewStream.ToProperty(this, nameof(ContextualView)).DisposeWith(_disposable);

            Router = new RoutingState();

            Locator.CurrentMutable.RegisterConstant<IAppViewModel>(this);
            Locator.CurrentMutable.RegisterConstant<IScreen>(this);

            Platform.ViewService.Navigating += OnNavigatingCore;
            Platform.IxService.Changed+= OnIxContentChanged;
            Platform.DialogService.Showing += OnShowing;
            
            
            //
            // DisposeWith
            _currentViewModelStream.DisposeWith(_disposable);
            _quickViewStream.DisposeWith(_disposable);
            _toolViewStream.DisposeWith(_disposable);
            _contextualViewStream.DisposeWith(_disposable);
            _extraViewStream.DisposeWith(_disposable);
        }

        private void OnShowing(object sender, DialogShowingEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnIxContentChanged(object sender, IxContentChangedEventArgs e)
        {
            
        }

        private void OnNavigatingCore(object sender, NavigateToViewEventArgs e)
        {
            _currentViewModelStream.OnNext(e.Current);
            OnNavigating(sender, e);
        }

        protected virtual void OnNavigating(object sender, NavigateToViewEventArgs e)
        {
        }

        public IQuickViewModel ToolView => _toolView.Value;
        public IQuickViewModel QuickView => _quickView.Value;
        public IQuickViewModel ExtraView => _extraView.Value;
        public IQuickViewModel ContextualView => _contextualView.Value;
        public IPageViewModel Current => _currentViewModel.Value;
        public RoutingState Router { get; }
    }
}