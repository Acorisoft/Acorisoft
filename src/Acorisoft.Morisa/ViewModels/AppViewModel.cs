using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Emotions;
using Acorisoft.Morisa.Persistants;
using Acorisoft.Morisa.Internal;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        private readonly IApplicationEnvironment _AppEnv;
        private readonly ICompositionSetManager  _CompositionSetManager;

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public AppViewModel(ICompositionSetManager csMgr, IEnumerable<IMechanismCore> mechanisms)
        {
            _CompositionSetManager = csMgr;

            Observable.FromEventPattern<CompositionSetChangedEventArgs>(_CompositionSetManager, "Changed")
                      .ObserveOn(RxApp.MainThreadScheduler)
                      .Subscribe(x =>
                      {
                          if(mechanisms != null)
                          {
                              foreach(var mechanism in mechanisms)
                              {
                                  mechanism.Input.OnNext(x.EventArgs.NewValue);
                                  mechanism.Input.OnCompleted();
                              }
                          }
                      });

            Observable.FromEventPattern<CompositionSetOpenedEventArgs>(_CompositionSetManager, "Opened")
                      .ObserveOn(RxApp.MainThreadScheduler)
                      .Subscribe(x =>
                      {

                      });
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 获取全局的设定集管理器
        /// </summary>
        public ICompositionSetManager CompositionSetManager => _CompositionSetManager;

        //-------------------------------------------------------------------------------------------------
        //
        //  Setting Properties
        //
        //-------------------------------------------------------------------------------------------------
    }
}
