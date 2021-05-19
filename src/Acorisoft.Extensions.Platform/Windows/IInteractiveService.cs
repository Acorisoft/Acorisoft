using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    public interface IInteractiveService
    {
        void SetQuickView(IQuickViewModel quickViewModel);
        void SetContextualView(IQuickViewModel quickViewModel);
        void SetToolView(IQuickViewModel quickViewModel);
        void SetExtraView(IQuickViewModel quickViewModel);

        event IxContentChangedEventHandler Changed;
    }
}