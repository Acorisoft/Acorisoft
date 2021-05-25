namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public class QuickViewModelBase : ViewModelBase, IQuickViewModel
    {
        void IQuickViewModel.Start(IPageViewModel currentViewModel)
        {
            OnStart(currentViewModel);
        }
        
        protected virtual void OnStart(IPageViewModel currentViewModel)
        {
        }
    }
}