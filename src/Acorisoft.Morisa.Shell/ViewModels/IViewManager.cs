using Acorisoft.Views;
using ReactiveUI;
using System;

namespace Acorisoft.ViewModels
{
    public interface IViewManager
    {
        public void DisplayViewFor<TViewModel>() where TViewModel : ViewModelBase, IViewModel;

        public void DisplayViewFor<TViewModel>(INavigateParameter parameters) where TViewModel : ViewModelBase, IViewModel;

        public void DisplayViewFor(IViewModel vm);
        public void DisplayViewFor(IViewModel vm, INavigateParameter parameters);

        public void DisplayViewFor(Type vm);
    }
}