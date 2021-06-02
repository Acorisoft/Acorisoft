namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public interface IDialogViewModel : IViewModel
    {
        bool Accept<T>();
        object GetResult();
        bool VerifyAccess();
        
        bool CanCancel();
    }
}