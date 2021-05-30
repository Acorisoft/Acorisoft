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
        private static readonly Lazy<AppViewModel> LazyInstance = new Lazy<AppViewModel>(() => Locator.Current.GetService<AppViewModel>());

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
            where TViewModel : QuickViewModelBase, IDialogViewModel where TView : IViewFor<TViewModel>
        {
            container.Register<IViewFor<TViewModel>, TView>();
            container.Register<TViewModel>();
        }

        public static void RegisterStickyNote(this IContainer container)
        {
            container.RegisterViewModelAndView<StickyNoteGalleryViewModel,StickyNoteGalleryView>();
            container.RegisterViewModelAndView<StickyNoteViewModel,StickyNoteView>();
        }
        
        public static AppViewModel AppViewModel => LazyInstance.Value;
    }
}