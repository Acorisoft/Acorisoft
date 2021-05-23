using System;
using Acorisoft.Extensions.Platforms.Services;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public static class ServiceLocator
    {
        private static readonly Lazy<IViewService> LazyViewServiceInstance = new Lazy<IViewService>(()=>Locator.Current.GetService<IViewService>());
        private static readonly Lazy<IDialogService> LazyDialogServiceInstance = new Lazy<IDialogService>(()=>Locator.Current.GetService<IDialogService>());
        
        public static IViewService ViewService => LazyViewServiceInstance.Value;
        public static IDialogService DialogService => LazyDialogServiceInstance.Value;
    }
}