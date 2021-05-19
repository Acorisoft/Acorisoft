using DryIoc;
using Splat.DryIoc;

namespace Acorisoft.Extensions.Windows.Platforms
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 初始化容器服务
        /// </summary>
        /// <returns></returns>
        public static IContainer Init()
        {
            var container = new Container(Rules.Default.WithTrackingDisposableTransients());
            container.UseDryIocDependencyResolver();
            container.RegisterInstance<IViewService>(new ViewService());
            container.RegisterInstance<IDialogService>(new DialogService());
            container.RegisterInstance<IToastService>(new ToastService());
            container.RegisterInstance<IInteractiveService>(new InteractiveService());
            
            ServiceProvider.SetServiceProvider(container);
            return container;
        }
    }
}