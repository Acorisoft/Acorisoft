using System;
using System.Collections.Generic;
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

        public AppViewModel(IViewService service, ICompositionSetRequestQueue requestQueue) : base(service)
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
            disposablePoe.DisposeWith(_disposable);
            disposablePos.DisposeWith(_disposable);
        }
    }
}