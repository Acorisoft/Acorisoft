using Acorisoft.Extensions.Platforms.Services;
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
            
            ServiceProvider.SetServiceProvider(container);
            return container;
        }
    }
}