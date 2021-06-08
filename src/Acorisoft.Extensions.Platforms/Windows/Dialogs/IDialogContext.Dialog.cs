using System;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Dialogs
{
    internal class NormalDialogContext : DialogContextBase, IDialogContext
    {
        public NormalDialogContext(IDialogViewModel viewModel, IDialogEventRaiser raiser) : base(viewModel, raiser)
        {
            TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
            Session = new DialogSession();
        }

        public override void NextOrComplete()
        {
            Session.SetIsCompleted(true);
            Session.SetResult(ViewModel);
            TaskCompletionSource.SetResult(Session);
            Raiser.Close();
        }

        public override void Cancel()
        {
            Session.SetIsCompleted(false);
            Session.SetResult(ViewModel);
            TaskCompletionSource.SetResult(Session);
            Raiser.Close();
        }
        
        internal DialogSession Session { get; }

        internal TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }
        internal Task<IDialogSession> Task => TaskCompletionSource.Task;
    }
}