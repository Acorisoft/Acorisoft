namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IQuickViewModel : IViewModel 
    {
        void Start(IPageViewModel parent);
    }
}