using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.ProjectSystem;
using LiteDB;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        private readonly CompositeDisposable _disposable;
        private readonly ICompositionSetManager _compositionSetManager;
        private readonly ICompositionSetFileManager _fileManager;
        private readonly ObservableAsPropertyHelper<bool> _isOpen;
        public AppViewModel(IViewService service,
            ICompositionSetRequestQueue requestQueue, 
            ICompositionSetFileManager fileManager,
            ICompositionSetManager compositionSetManager) : base(service)
        {
            if (requestQueue == null)
            {
                throw new ArgumentNullException(nameof(requestQueue));
            }

            var disposablePos = requestQueue.Requesting
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualStartBusyState("打开项目"); });

            var disposablePoe = requestQueue.Responding
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualEndBusyState(); });

            _disposable = new CompositeDisposable();
            _fileManager = fileManager;
            _compositionSetManager = compositionSetManager;
            _isOpen = _compositionSetManager.IsOpen.ToProperty(this,nameof(IsOpen));
            
            
            
            _disposable.Add(_compositionSetManager);
            disposablePoe.DisposeWith(_disposable);
            disposablePos.DisposeWith(_disposable);
        }

        /// <summary>
        /// 获取或设置当前打开的项目
        /// </summary>
        public ReadOnlyObservableCollection<ICompositionSet> CompositionSets => _compositionSetManager.CompositionSets;
        
        /// <summary>
        /// 获取或设置当前的可打开项目。
        /// </summary>
        public bool IsOpen => _isOpen.Value;
    }
}