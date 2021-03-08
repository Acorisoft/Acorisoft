using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Reactive.Threading;
using Acorisoft.Morisa.Core;

namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private readonly CompositeDisposable _disposable;

        public AppViewModel(IMorisaProjectManager projectMgr,IEnumerable<IEntityService> entitySrves)
        {
            //
            // when project manager load an new project 
            // it will return the project info back to the vm
            // and we can set the current project to database
            projectMgr.ProjectInfo
                      .Subscribe(x => CurrentProject = x)
                      .DisposeWith(_disposable);

            //
            // when project manager load an new project
            // it will update all entity service
            projectMgr.Project
                      .SubscribeOn(RxApp.TaskpoolScheduler)
                      .Subscribe(x =>
                      {
                          foreach(var srv in entitySrves)
                          {
                              srv.OnNext(x);
                              srv.OnCompleted();
                          }
                      })
                      .DisposeWith(_disposable);
        }

        public IMorisaProjectInfo CurrentProject { get; set; }
    }
}
