using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Dialogs
{
    public interface IDialogContext
    {
        void Cancel();
        void NextOrComplete();
        bool VerifyAccess();
        bool CanCancel();
    }

    public interface IStackedDialogContext : IDialogContext
    {
        bool CanLast();
        IViewModel Share { get; }
        int Count { get; }
        int CurrentIndex { get; }
    }
}