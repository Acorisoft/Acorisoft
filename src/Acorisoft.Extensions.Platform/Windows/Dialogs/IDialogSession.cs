namespace Acorisoft.Extensions.Windows.Dialogs
{
    public interface IDialogSession
    {
        bool IsCompleted { get; }
        object Result { get; }
    }
}