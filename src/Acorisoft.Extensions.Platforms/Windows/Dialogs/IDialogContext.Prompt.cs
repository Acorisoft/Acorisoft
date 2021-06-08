using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Dialogs
{
    internal class PromptDialogContext : DialogContextBase
    {
        public PromptDialogContext(IDialogViewModel viewModel, IDialogEventRaiser raiser) : base(viewModel, raiser)
        {
            TaskCompletionSource = new TaskCompletionSource<bool?>();
        }

        public sealed override void Cancel()
        {
            TaskCompletionSource.SetResult(false);
            Raiser.Close();
        }

        public sealed override void NextOrComplete()
        {
            TaskCompletionSource.SetResult(true);
            Raiser.Close();
        }

        internal TaskCompletionSource<bool?> TaskCompletionSource { get; }
        internal Task<bool?> Task => TaskCompletionSource.Task;
    }
}