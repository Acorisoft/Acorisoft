using System;
using DryIoc;
using MediatR;
using Splat;
using Splat.DryIoc;

namespace Acorisoft.Extensions.Windows.Services
{
    public static class ServiceLocator
    {
        private static readonly IContainer Container;
        
        static ServiceLocator()
        {
            Container = new Container(Rules.Default.WithTrackingDisposableTransients());
            Container.UseDryIocDependencyResolver();
            
        }
        private static readonly Lazy<IViewService> ReadOnlyViewService = new Lazy<IViewService>(LocateViewService);
        private static IViewService LocateViewService() => Locator.Current.GetService<IViewService>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            var factory = new ServiceFactory(Container.Resolve);
            var mediator = new Mediator(factory);
                
            //
            // 注册中介者
            Container.RegisterInstance<Mediator>(mediator);
            Container.UseInstance<IMediator>(mediator);
            
            
            //
            // 注册视图服务
            var vs = new ViewService(mediator);
            Container.RegisterInstance<ViewService>(vs);
            Container.UseInstance<IViewService>(vs);
        }
        
        /// <summary>
        /// 获取当前应用程序注册的视图服务。
        /// </summary>
        public static IViewService ViewService => ReadOnlyViewService.Value;
    }
}