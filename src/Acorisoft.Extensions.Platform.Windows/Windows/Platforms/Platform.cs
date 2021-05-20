using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ReactiveUI;

namespace Acorisoft.Extensions.Windows.Platforms
{
    public static class Platform
    {
        internal static void VerifyAccess()
        {
        }

        public static string Bin => Environment.CurrentDirectory;
        public static string Logs => Path.Combine(Directory.GetParent(Bin)?.FullName ?? Bin, "Logs");
        public static string Plugins => Path.Combine(Directory.GetParent(Bin)?.FullName ?? Bin, "Plugins");
        public static string Patches => Path.Combine(Directory.GetParent(Bin)?.FullName ?? Bin, "Patches");
        public static string Cache => Path.Combine(Directory.GetParent(Bin)?.FullName ?? Bin, "Cache");

        #region Services Properties

        internal static Lazy<T> CreateLazyMode<T>() =>
            new Lazy<T>(() => (T) ServiceProvider.Provider.GetService(typeof(T)));

        private static readonly Lazy<IViewService> LazyViewService = CreateLazyMode<IViewService>();
        private static readonly Lazy<IDialogService> LazyDialogService = CreateLazyMode<IDialogService>();
        private static readonly Lazy<IToastService> LazyToastService = CreateLazyMode<IToastService>();
        private static readonly Lazy<IScreen> LazyScreeen = CreateLazyMode<IScreen>();
        private static readonly Lazy<IInteractiveService> LazyIxService = CreateLazyMode<IInteractiveService>();


        [NotNull] public static IScreen ScreenService => LazyScreeen.Value;
        [NotNull] public static IViewService ViewService => LazyViewService.Value;

        [NotNull] public static IDialogService DialogService => LazyDialogService.Value;

        [NotNull] public static IToastService ToastService => LazyToastService.Value;
        [NotNull] public static IInteractiveService IxService => LazyIxService.Value;

        #endregion
    }
}