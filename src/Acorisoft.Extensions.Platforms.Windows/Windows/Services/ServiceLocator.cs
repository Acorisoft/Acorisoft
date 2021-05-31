using System;
using Acorisoft.Extensions.Platforms.Languages;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Studio.ProjectSystem;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public static class ServiceLocator
    {
        private static readonly Lazy<IViewService> LazyViewServiceInstance = new Lazy<IViewService>(()=>Locator.Current.GetService<IViewService>());
        private static readonly Lazy<IDialogService> LazyDialogServiceInstance = new Lazy<IDialogService>(()=>Locator.Current.GetService<IDialogService>());
        private static readonly Lazy<ICompositionSetFileManager> LazyCompositionSetFileManagerInstance = new Lazy<ICompositionSetFileManager>(()=>Locator.Current.GetService<ICompositionSetFileManager>());
        private static readonly Lazy<ICompositionSetManager> LazyCompositionSetManagerInstance = new Lazy<ICompositionSetManager>(()=>Locator.Current.GetService<ICompositionSetManager>());
        private static readonly Lazy<ILanguageService> InternalLanguageInstance = new Lazy<ILanguageService>(new LanguageService(SR.ResourceManager));

        internal static ILanguageService InternalLanguageService => InternalLanguageInstance.Value;
        public static ICompositionSetManager CompositionSetManager => LazyCompositionSetManagerInstance.Value;
        public static IViewService ViewService => LazyViewServiceInstance.Value;
        public static IDialogService DialogService => LazyDialogServiceInstance.Value;
        public static ICompositionSetFileManager FileManagerService => LazyCompositionSetFileManagerInstance.Value;
    }
}