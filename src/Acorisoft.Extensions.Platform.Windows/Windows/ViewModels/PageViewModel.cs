using ReactiveUI;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class PageViewModel : ViewModelBase, IPageViewModel
    {
        public virtual bool KeepAlive => true;
    }
}