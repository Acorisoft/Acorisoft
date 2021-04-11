namespace Acorisoft.Dialogs
{
    public interface IDialogSession
    {
        public T GetResult<T>();
        public bool IsCompleted { get;}
    }
}