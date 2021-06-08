namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public interface IDialogService
    {
        bool CanNextOrComplete();
        bool CanIgnoreOrSkip();
        bool CanCancel();
        bool CanLast();
        void IgnoreOrSkip();
        void Last();
        void Cancel();
        void NextOrComplete();
    }
}