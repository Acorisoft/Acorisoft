using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Engines;
using LiteDB;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        private readonly CompositeDisposable _disposable;
        private readonly IDisposable _disposablePos,_disposablePoe;
        
        public AppViewModel(IViewService service, IDocumentEngineAquirement aquirement) : base(service)
        {
            if (aquirement == null)
            {
                throw new ArgumentNullException(nameof(aquirement));
            }

            _disposablePos = aquirement.ProjectOpenStarting
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
            {
                service.ManualStartBusyState("打开项目");
            });

            _disposablePoe = aquirement.ProjectOpenEnding
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    service.ManualEndBusyState();
                });

            _disposable = new CompositeDisposable();
            _disposablePoe.DisposeWith(_disposable);
            _disposablePos.DisposeWith(_disposable);
        }
    }
}