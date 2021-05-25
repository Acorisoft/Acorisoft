namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public interface IQuickViewModel : IViewModel
    {
        void Start(IPageViewModel currentViewModel);
    }
}