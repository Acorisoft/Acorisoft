using System;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.ViewModels;
using Acorisoft.Studio.Views;
using DryIoc;
using ImTools;
using ReactiveUI;
using Splat;

namespace Acorisoft.Studio
{
    public static class ViewModelLocator
    {
        #region Lazy Field

        

        private static readonly Lazy<AppViewModel> LazyInstance =
            new Lazy<AppViewModel>(() => Locator.Current.GetService<AppViewModel>());

        #endregion

        #region Insfrastructure

        

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

        public static void RegisterViewModelAndQuick<TViewModel, TView>(this IContainer container)
            where TViewModel : QuickViewModelBase, IQuickViewModel where TView : IViewFor<TViewModel>
        {
            container.Register<IViewFor<TViewModel>, TView>();
            container.Register<TViewModel>();
        }

        #endregion
        public static void RegisterCommonViewModelAndViews(this IContainer container)
        {
            container.RegisterViewModelAndDialog<PageItemCountViewModel, PageItemCountView>();
        }

        public static void RegisterHome(this IContainer container)
        {
            RegisterViewModelAndView<HomeViewModel, HomeView>(container);
            RegisterViewModelAndQuick<HomeContextViewModel, HomeContextView>(container);
            RegisterViewModelAndDialog<NewProjectDialogViewModel, NewProjectDialog>(container);
        }

        public static void RegisterStickyNote(this IContainer container)
        {
            container.RegisterViewModelAndView<StickyNoteGallery, StickyNoteGalleryView>();
            container.RegisterViewModelAndView<StickyNoteViewModel, StickyNoteView>();
        }

        public static AppViewModel AppViewModel => LazyInstance.Value;
    }
}