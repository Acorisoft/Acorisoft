using System.Reactive.Disposables;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.ProjectSystem;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeViewModel : PageViewModelBase
    {
        private readonly ObservableAsPropertyHelper<ICompositionSet> _compositionSet;
        private readonly CompositeDisposable _disposable;
        public HomeViewModel(ICompositionSetManager compositionSetManager)
        {
            ViewAware.SetContextView<HomeContextViewModel>();
            _disposable = new CompositeDisposable();
            _compositionSet = compositionSetManager.Composition.ToProperty(this, nameof(CompositionSet)).DisposeWith(_disposable);
        }

        public ICompositionSet CompositionSet => _compositionSet.Value;
    }
}