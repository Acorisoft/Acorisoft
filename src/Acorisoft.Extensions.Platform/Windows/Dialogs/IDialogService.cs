using System;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Dialogs;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    public interface IDialogService
    {
        Task<bool?> Prompt(IDialogViewModel viewModel);
        Task<IDialogSession> ShowDialog(IDialogViewModel viewModel);
        Task<IDialogSession> ShowWizard(IDialogContext context);
        event EventHandler<DialogChangedEventArgs> DialogChanged; 
        event DialogShowingEventHandler DialogShowing;
        event WizardShowingEventHandler WizardShowing;
        event PromptShowingEventHandler PromptShowing;
        event EventHandler DialogClosing;
    }
}