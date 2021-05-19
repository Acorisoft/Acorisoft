using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Dialogs;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    class DialogService : IDialogService
    {
        public Task<bool?> Prompt(IDialogViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDialogSession> ShowDialog(IDialogViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDialogSession> ShowWizard(IDialogContext context)
        {
            throw new System.NotImplementedException();
        }

        public event DialogShowingEventHandler Showing;
    }
}