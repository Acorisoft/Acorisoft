using System;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Dialogs
{
    public abstract class DialogContextBase : IDialogContext
    {
        internal DialogContextBase(IDialogEventRaiser raiser)
        {
            Raiser = raiser ?? throw new ArgumentNullException(nameof(raiser));
        }
        internal DialogContextBase(IDialogViewModel viewModel, IDialogEventRaiser raiser)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            Raiser = raiser ?? throw new ArgumentNullException(nameof(raiser));
        }

        public virtual void NextOrComplete()
        {
            
        }

        public virtual void Cancel()
        {
            
        }
        
                
        public bool VerifyAccess()
        {
            return ViewModel.VerifyAccess();
        }

        public bool CanCancel()
        {
            return ViewModel.CanCancel();
        }
        
        internal IDialogEventRaiser Raiser { get; }
        public IDialogViewModel ViewModel { get; protected set; }
    }
}