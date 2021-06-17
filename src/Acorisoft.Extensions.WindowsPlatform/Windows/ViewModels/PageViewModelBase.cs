using ReactiveUI;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class PageViewModelBase : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}