namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IPageViewModel : IViewModel
    {
        bool KeepAlive { get; }
    }
}