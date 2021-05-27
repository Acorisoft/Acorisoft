namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public interface IPageViewModel : IViewModel, IViewModelParameter
    {
        string Title { get; }
        
    }
}