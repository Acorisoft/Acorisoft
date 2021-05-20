using System;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Dialogs;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    internal interface IDialogEventVisitor
    {
        void RaiseDialogClose();
        void RaiseDialogChanged(IDialogViewModel viewModel);
    }


    class DialogService : IDialogService, IDialogEventVisitor
    {
        void IDialogEventVisitor.RaiseDialogClose()
        {
            DialogClosing?.Invoke(this, new EventArgs());
        }

        void IDialogEventVisitor.RaiseDialogChanged(IDialogViewModel viewModel)
        {
            DialogChanged?.Invoke(this, new DialogChangedEventArgs(viewModel));
        }

        //
        // 主要的问题是如何解耦合
        //
        // 1) 通知属性更改
        // 2) 
        public Task<bool?> Prompt(IDialogViewModel viewModel)
        {
            if (viewModel is null)
            {
                return Task.Run(() => (bool?) null);
            }

            var prompt = new PromptDialogContext(viewModel, this);
            PromptShowing?.Invoke(this, new PromptShowingEventArgs(prompt));
            return prompt.Task;
        }

        public Task<IDialogSession> ShowDialog(IDialogViewModel viewModel)
        {
            if (viewModel is null)
            {
                return Task.Run(() => (IDialogSession) new DialogSession());
            }

            var dialog = new DefaultDialogContext(viewModel, this);
            DialogShowing?.Invoke(this, new DialogShowingEventArgs(dialog));
            return dialog.Task;
        }

        public Task<IDialogSession> ShowWizard(IDialogContext context)
        {
            if (context is null)
            {
                return Task.Run(() => (IDialogSession) new DialogSession());
            }

            var dialog = new StackedDialogContext(context, this);
            WizardShowing?.Invoke(this, new WizardShowingEventArgs(dialog));
            return dialog.Task;
        }

        public event EventHandler<DialogChangedEventArgs> DialogChanged;
        public event PromptShowingEventHandler PromptShowing;
        public event WizardShowingEventHandler WizardShowing;
        public event DialogShowingEventHandler DialogShowing;
        public event EventHandler DialogClosing;
    }
}