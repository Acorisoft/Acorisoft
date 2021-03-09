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
using LiteDB;

namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

        protected class Setting
        {

        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly CompositeDisposable    _Disposable;
        private readonly LiteDatabase           _AppDB;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------


        public AppViewModel(IMorisaProjectManager projectMgr , IEnumerable<IEntityService> entitySrves)
        {
            //
            // when project manager load an new project 
            // it will return the project info back to the vm
            // and we can set the current project to database
            projectMgr.ProjectInfo
                      .Subscribe(x => CurrentProject = x)
                      .DisposeWith(_Disposable);

            //
            // when project manager load an new project
            // it will update all entity service
            projectMgr.Project
                      .SubscribeOn(RxApp.TaskpoolScheduler)
                      .Subscribe(x =>
                      {
                          foreach (var srv in entitySrves)
                          {
                              var observer = srv.ProjectObserver;
                              observer.OnNext(x);
                              observer.OnCompleted();
                          }
                      })
                      .DisposeWith(_Disposable);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------
        public IMorisaProjectInfo CurrentProject { get; set; }
    }
}
