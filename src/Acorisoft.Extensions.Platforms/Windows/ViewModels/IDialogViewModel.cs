namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public interface IDialogViewModel : IViewModel
    {
        bool VerifyAccess();
        
        bool CanCancel();
    }
}