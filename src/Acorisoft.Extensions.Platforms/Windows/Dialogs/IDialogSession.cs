using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Dialogs
{
    public interface IDialogSession
    {
        bool IsCompleted { get; }
        IViewModel Result { get; }
    }

    public class DialogSession : IDialogSession
    {
        internal void SetIsCompleted(bool value) => IsCompleted = value;
        internal void SetResult(IViewModel value) => Result = value;
        public bool IsCompleted { get; private set; }

        public IViewModel Result { get; private set; }
    }
}