namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IDialogViewModel : IViewModel
    {
        bool VerifyAccess();
        bool CanIgnore();
        
        string Subtitle { get; }
    }
}