// using System;
// using System.Globalization;
// using System.Reactive.Disposables;
// using System.Reactive.Linq;
// using System.Reactive.Subjects;
// using System.Threading.Tasks;
// using System.Windows.Input;
// using Acorisoft.Extensions.Windows.Platforms;
// using ReactiveUI;
// using Splat;
//
// namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
// {
//     public abstract class AppViewModelBase : ViewModelBase, IScreen, IAppViewModel
//     {
//         private readonly CompositeDisposable _disposable;
//
//         private readonly Subject<IPageViewModel> _currentViewModelStream;
//         private readonly Subject<IQuickViewModel> _quickViewStream;
//         private readonly Subject<IQuickViewModel> _toolViewStream;
//         private readonly Subject<IQuickViewModel> _contextualViewStream;
//         private readonly Subject<IQuickViewModel> _extraViewStream;
//         private readonly Subject<IDialogViewModel> _dialogStream;
//         private readonly Subject<bool> _isBusyStream;
//         private readonly Subject<string> _descriptionStream;
//         
//         private readonly ObservableAsPropertyHelper<IDialogViewModel> _dialog;
//         private readonly ObservableAsPropertyHelper<IPageViewModel> _currentViewModel;
//         private readonly ObservableAsPropertyHelper<IQuickViewModel> _quickView;
//         private readonly ObservableAsPropertyHelper<IQuickViewModel> _toolView;
//         private readonly ObservableAsPropertyHelper<IQuickViewModel> _extraView;
//         private readonly ObservableAsPropertyHelper<IQuickViewModel> _contextualView;
//         private readonly ObservableAsPropertyHelper<bool> _isBusy;
//         private readonly ObservableAsPropertyHelper<string> _description;
//
//         protected AppViewModelBase()
//         {
//             _disposable = new CompositeDisposable();
//             _currentViewModelStream = new Subject<IPageViewModel>();
//             _quickViewStream = new Subject<IQuickViewModel>();
//             _toolViewStream = new Subject<IQuickViewModel>();
//             _contextualViewStream = new Subject<IQuickViewModel>();
//             _extraViewStream = new Subject<IQuickViewModel>();
//             _dialogStream = new Subject<IDialogViewModel>();
//             _isBusyStream = new Subject<bool>();
//             _descriptionStream = new Subject<string>();
//
//             _dialog = _dialogStream.ToProperty(this, nameof(Dialog)).DisposeWith(_disposable);
//             _currentViewModel = _currentViewModelStream.ToProperty(this, nameof(Current)).DisposeWith(_disposable);
//             _quickView = _quickViewStream.ToProperty(this, nameof(QuickView)).DisposeWith(_disposable);
//             _toolView = _toolViewStream.ToProperty(this, nameof(ToolView)).DisposeWith(_disposable);
//             _extraView = _contextualViewStream.ToProperty(this, nameof(ExtraView)).DisposeWith(_disposable);
//             _contextualView = _extraViewStream.ToProperty(this, nameof(ContextualView)).DisposeWith(_disposable);
//             _isBusy = _isBusyStream.ToProperty(this, nameof(IsBusy)).DisposeWith(_disposable);
//             _description = _descriptionStream.ToProperty(this, nameof(Description)).DisposeWith(_disposable);
//
//             Router = new RoutingState();
//
//             Locator.CurrentMutable.RegisterConstant<IAppViewModel>(this);
//             Locator.CurrentMutable.RegisterConstant<IScreen>(this);
//             
//             //
//             // DisposeWith
//             _currentViewModelStream.DisposeWith(_disposable);
//             _quickViewStream.DisposeWith(_disposable);
//             _toolViewStream.DisposeWith(_disposable);
//             _contextualViewStream.DisposeWith(_disposable);
//             _extraViewStream.DisposeWith(_disposable);
//             _dialogStream.DisposeWith(_disposable);
//             _isBusyStream.DisposeWith(_disposable);
//             _descriptionStream.DisposeWith(_disposable);
//         }
//
//        
//         private void OnIxContentChanged(object sender, IxContentChangedEventArgs e)
//         {
//             if (e.ContextualView is not null)
//             {
//                 e.ContextualView.Start(_currentViewModel.Value);
//                 _contextualViewStream.OnNext(e.ContextualView);
//             }
//
//             if (e.ExtraView is not null)
//             {
//                 e.ExtraView.Start(_currentViewModel.Value);
//                 _extraViewStream.OnNext(e.ExtraView);
//             }
//
//             if (e.QuickView is not null)
//             {
//                 e.QuickView.Start(_currentViewModel.Value);
//                 _quickViewStream.OnNext(e.QuickView);
//             }
//
//             if (e.ToolView is not null)
//             {
//                 e.ToolView.Start(_currentViewModel.Value);
//                 _toolViewStream.OnNext(e.ToolView);
//             }
//         }
//
//         private void OnNavigatingCore(object sender, NavigateToViewEventArgs e)
//         {
//             switch (e.Current)
//             {
//                 case PageViewModel page:
//                     _currentViewModelStream.OnNext(page);
//                     OnNavigating(sender, e);
//                     Router.Navigate.Execute(page);
//                     break;
//                 case SplashViewModel splash:
//                     Router.Navigate.Execute(splash);
//                     break;
//             }
//         }
//
//         protected virtual void OnNavigating(object sender, NavigateToViewEventArgs e)
//         {
//         }
//
//         public bool IsBusy => _isBusy.Value;
//         public string Description => _description.Value;
//         public IDialogViewModel Dialog => _dialog.Value;
//         public IQuickViewModel ToolView => _toolView.Value;
//         public IQuickViewModel QuickView => _quickView.Value;
//         public IQuickViewModel ExtraView => _extraView.Value;
//         public IQuickViewModel ContextualView => _contextualView.Value;
//         public IPageViewModel Current => _currentViewModel.Value;
//         public RoutingState Router { get; }
//     }
// }