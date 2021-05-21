namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class DialogViewModel : ViewModelBase,IDialogViewModel
    {
        public virtual bool VerifyAccess()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanIgnore()
        {
            throw new System.NotImplementedException();
        }

    }
}