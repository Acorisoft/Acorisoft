using ReactiveUI;
using ReactiveUI.Validation.Helpers;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class DialogViewModelBase : ReactiveValidationObject, IRoutableViewModel, IDialogViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}