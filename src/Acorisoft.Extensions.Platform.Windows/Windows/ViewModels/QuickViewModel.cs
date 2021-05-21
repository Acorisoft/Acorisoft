using System;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class QuickViewModel : ViewModelBase, IQuickViewModel 
    {
        private IPageViewModel _parent;

        void IQuickViewModel.Start(IPageViewModel parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public IPageViewModel Parent
        {
            get => _parent;
            private set => Set(ref _parent, value);
        }
    }
}