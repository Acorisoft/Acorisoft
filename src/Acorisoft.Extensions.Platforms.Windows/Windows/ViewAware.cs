using System;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public static class ViewAware
    {
        #region InteractiveView

        public static void SetQuickView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                Locator.Current.GetService<TViewModel>(),
                null,
                null,
                null);
        }

        public static void SetExtraView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                null,
                null,
                Locator.Current.GetService<TViewModel>());
        }

        public static void SetToolView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                Locator.Current.GetService<TViewModel>(),
                null,
                null);
        }

        public static void SetContextView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                null,
                Locator.Current.GetService<TViewModel>(),
                null);
        }

        public static void SetInteractiveView<TQuickViewModel, TContextViewModel, TToolViewModel, TExtraViewModel>()
            where TQuickViewModel : QuickViewModelBase, IQuickViewModel
            where TContextViewModel : QuickViewModelBase, IQuickViewModel
            where TToolViewModel : QuickViewModelBase, IQuickViewModel
            where TExtraViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                Locator.Current.GetService<TQuickViewModel>(),
                Locator.Current.GetService<TContextViewModel>(),
                Locator.Current.GetService<TToolViewModel>(),
                Locator.Current.GetService<TExtraViewModel>());
        }

        #endregion

        class ManualBusyState : IDisposable
        {
            public ManualBusyState(string description)
            {
                ServiceLocator.ViewService.ManualStartBusyState(description);
            }

            public void Dispose()
            {
                ServiceLocator.ViewService.ManualEndBusyState();
            }
        }
        
        public static void NavigateTo<TViewModel>() where TViewModel : PageViewModelBase, IPageViewModel
        {
            var vm = Locator.Current.GetService<TViewModel>();
            ServiceLocator.ViewService.NavigateTo(vm);
        }

        public static Task<IDialogSession> ShowDialog<TViewModel>() where TViewModel : DialogViewModelBase, IDialogViewModel
        {
            var vm = Locator.Current.GetService<TViewModel>();
            return ServiceLocator.ViewService.ShowDialog(vm);
        }

        public static void Toast(string message)
        {
            ServiceLocator.ViewService.Toast(message);
        }
        
        public static void Toast(Exception ex)
        {
            ServiceLocator.ViewService.Toast(ex.Message);
        }

        public static IDisposable ForceBusyState(string description)
        {
            return new ManualBusyState(description);
        }
    }
}