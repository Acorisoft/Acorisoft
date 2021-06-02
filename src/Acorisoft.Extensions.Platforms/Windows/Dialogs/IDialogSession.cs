using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Dialogs
{
    public interface IDialogSession
    {
        T GetResult<T>();
        bool IsCompleted { get; }
        IViewModel Result { get; }
    }

    public class DialogSession : IDialogSession
    {
        public T GetResult<T>()
        {
            if (Result is IDialogViewModel dialogViewModel && dialogViewModel.Accept<T>())
            {
                return (T)dialogViewModel.GetResult();
            }
            return default(T);
        }
        internal void SetIsCompleted(bool value) => IsCompleted = value;
        internal void SetResult(IViewModel value) => Result = value;
        public bool IsCompleted { get; private set; }

        public IViewModel Result { get; private set; }
    }
}