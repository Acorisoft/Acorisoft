using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Extensions.Platforms.Windows.Views;
using DryIoc;
using ReactiveUI;
using Splat.DryIoc;

namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public static class Platform
    {
        public static IContainer Init()
        {
            var container = new Container(Rules.Default.WithTrackingDisposableTransients());
            var viewService = new ViewService();
            container.RegisterInstance(viewService);
            container.UseInstance<IViewService>(viewService, IfAlreadyRegistered.AppendNewImplementation);
            container.UseInstance<IDialogService>(viewService, IfAlreadyRegistered.AppendNewImplementation);
            container.UseDryIocDependencyResolver();
            container.RegisterViewModelAndDialog<AwaitDeleteViewModel, AwaitDeleteView>();
            container.RegisterViewModelAndDialog<AwaitClosePageViewModel, AwaitClosePageView>();
            container.RegisterViewModelAndDialog<AwaitCloseWindowViewModel, AwaitCloseWindowView>();
            ServiceProvider.SetServiceProvider(container);
            return container;
        }
        
        public static void RegisterViewModelAndView<TViewModel, TView>(this IContainer container)
            where TViewModel : PageViewModelBase, IPageViewModel where TView : IViewFor<TViewModel>
        {
            container.Register<IViewFor<TViewModel>, TView>();
            container.Register<TViewModel>();
        }
        
        public static void RegisterViewModelAndDialog<TViewModel, TView>(this IContainer container)
            where TViewModel : DialogViewModelBase, IDialogViewModel where TView : IViewFor<TViewModel>
        {
            container.Register<IViewFor<TViewModel>, TView>();
            container.Register<TViewModel>();
        }
    }
}