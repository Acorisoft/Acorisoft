namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IQuickViewModel : IViewModel
    {
        IPageViewModel Parent { get; }
    }
}