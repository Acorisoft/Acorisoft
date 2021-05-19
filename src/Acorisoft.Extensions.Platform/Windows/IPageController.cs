namespace Acorisoft.Extensions.Windows
{
    public interface IPageController
    {
        bool CanNavigate(NavigateToViewEventArgs e);
    }
}