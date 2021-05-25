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
    }
}