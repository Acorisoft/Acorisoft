using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Joins;
using ReactiveUI;
using System.Reactive.Disposables;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.ProjectSystem;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeViewModel : PageViewModelBase
    {
        private readonly ObservableAsPropertyHelper<ICompositionSet> _compositionSet;
        private readonly ObservableAsPropertyHelper<ICompositionSetProperty> _compositionSetProperty;
        private readonly ICompositionSetManager _compositionSetManager;
        private readonly CompositeDisposable _disposable;
        
        public HomeViewModel(ICompositionSetManager compositionSetManager)
        {
            _compositionSetManager = compositionSetManager;
            _disposable = new CompositeDisposable();
            _compositionSet = compositionSetManager.Composition
                .ToProperty(this, nameof(CompositionSet))
                .DisposeWith(_disposable);
            _compositionSetProperty = compositionSetManager.Property.ToProperty(this, nameof(Property)).DisposeWith(_disposable);
            compositionSetManager.Property
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(OnPropertyChanged)
                .DisposeWith(_disposable);
        }

        private void OnPropertyChanged(ICompositionSetProperty property)
        {
            RaiseUpdated(nameof(Summary));
            RaiseUpdated(nameof(Name));
            RaiseUpdated(nameof(Cover));
        }

        private async void OpenCompositionSet(ICompositionSet compositionSet)
        {
            if (compositionSet == null)
            {
                return;
            }

            try
            {
                ServiceLocator.ViewService.ManualStartBusyState("正在打开项目");
                await _compositionSetManager.LoadProject(compositionSet);
            }
            catch
            {
                ServiceLocator.ViewService.Toast("打开失败");
            }
            finally
            {
                ServiceLocator.ViewService.ManualEndBusyState();
            }
        }
        
        protected override async void OnStart()
        {
            ViewAware.SetContextView<HomeContextViewModel>();

            try
            {
                ServiceLocator.ViewService.ManualStartBusyState("打开项目");
                
                await _compositionSetManager.LoadProject(_compositionSetManager.CompositionSets.FirstOrDefault());
            }
            catch
            {

            }
            finally
            {
                ServiceLocator.ViewService.ManualEndBusyState();
            }
            base.OnStart();
        }

        protected override void OnStop()
        {
            _disposable?.Dispose();
        }

        public ICompositionSet SelectedCompositionSet
        {
            get => _compositionSet.Value;
            set => OpenCompositionSet(value);
        }
        
        public ReadOnlyObservableCollection<ICompositionSet> CompositionSets => _compositionSetManager.CompositionSets; 
        public ICompositionSetProperty Property => _compositionSetProperty.Value;
        public ICompositionSet CompositionSet => _compositionSet.Value;
        public string Name
        {
            get => CompositionSet?.Property?.Name;
            set
            {
                CompositionSet.Property.Name = value;
                RaiseUpdated();
                ServiceLocator.CompositionSetManager
                    .PropertyManager
                    .SetProperty(CompositionSet.Property);
            }
            
        }
        
        public string Summary
        {
            get => CompositionSet?.Property?.Summary;
            set
            {
                CompositionSet.Property.Summary = value;
                RaiseUpdated();
                ServiceLocator.CompositionSetManager
                    .PropertyManager
                    .SetProperty(CompositionSet.Property);
            }
        }
        
        public Uri Cover
        {
            get => CompositionSet?.Property?.Cover;
        }
    }
}