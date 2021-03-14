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
using Acorisoft.Morisa.Collections;
using ObservableProjectCollection = Acorisoft.Morisa.Collections.ObservableUniqueCollection<Acorisoft.Morisa.ICompositionSetStore>;
using Acorisoft.Morisa.Dialogs;
using Splat;
using System.Windows;

namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------

        public const string ConnectionString = "FileName=App.Morisa-Setting;Mode=Shared;Initial Size=4MB";
        public const string ExternalCollectionName = "Externals";
        public const string SettingName = "Morisa.App.Setting";

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private IApplicationEnvironment             _AppEnv;
        private LiteCollection<BsonDocument>        _DB_Externals;
        private ObservableProjectCollection         _Projects;
        private IDialogManager                      _DialogManager;
        private readonly ICompositionSetManager     _CompositionSetManager;
        private readonly LiteDatabase               _AppDB;
        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public AppViewModel(ICompositionSetManager csMgr, IEnumerable<IMechanismCore> mechanisms)
        {
            _AppDB = new LiteDatabase(ConnectionString);
            _CompositionSetManager = csMgr;
            
            OnInitialize();
            Observable.FromEventPattern<CompositionSetChangedEventArgs>(_CompositionSetManager, "Changed")
                      .ObserveOn(RxApp.MainThreadScheduler)
                      .Subscribe(x =>
                      {
                          var cs = x.EventArgs.NewValue;
                          if(mechanisms != null)
                          {
                              foreach(var mechanism in mechanisms)
                              {
                                  mechanism.Input.OnNext(cs);
                                  mechanism.Input.OnCompleted();
                              }
                          }

                          CurrentProject = cs;
                      });

            Observable.FromEventPattern<CompositionSetOpenedEventArgs>(_CompositionSetManager, "Opened")
                      .ObserveOn(RxApp.MainThreadScheduler)
                      .Subscribe(x =>
                      {
                          var store = x.EventArgs.NewValue;
                          _AppEnv.CurrentProject = store;
                          _Projects.Add(store);
                          UpdateSetting();
                      });
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Methods
        //
        //-------------------------------------------------------------------------------------------------
        void OnInitialize()
        {
            if (_AppDB.CollectionExists(ExternalCollectionName))
            {
                try
                {
                    //
                    // 假如设置文件已经损坏
                    _DB_Externals = _AppDB.GetCollection(ExternalCollectionName);
                    _AppEnv = _DB_Externals.FindById(SettingName)
                                           .Deserialize<ApplicationEnvironment>();
                }
                catch
                {
                    _DB_Externals = _AppDB.GetCollection(ExternalCollectionName);
                    _AppEnv = CreateInstanceCore();
                    _DB_Externals.Upsert(SettingName, DatabaseMixins.Serialize((ApplicationEnvironment)_AppEnv));
                    MessageBox.Show("设置已经损坏，我们使用初始化设置重置");
                }
            }
            else
            {
                _DB_Externals = _AppDB.GetCollection(ExternalCollectionName);
                _AppEnv = CreateInstanceCore();
                _DB_Externals.Upsert(SettingName, DatabaseMixins.Serialize((ApplicationEnvironment)_AppEnv));
            }

            _Projects = new ObservableProjectCollection(_AppEnv.Projects);

            //
            // 打开项目
            if(_AppEnv.CurrentProject != null)
            {
                _CompositionSetManager.Load(_AppEnv.CurrentProject);
            }
        }

        public void UpdateSetting()
        {
            _DB_Externals.Upsert(SettingName, DatabaseMixins.Serialize(_AppEnv));
        }

        protected virtual ApplicationEnvironment CreateInstanceCore()
        {
            return new ApplicationEnvironment
            {
                IsFirstTime = true
            };
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        public IDialogManager DialogManager
        {
            get
            {
                if (_DialogManager == null)
                {
                    _DialogManager = Locator.Current.GetService<IDialogManager>();
                }
                return _DialogManager;
            }
        }

        /// <summary>
        /// 获取全局的设定集管理器
        /// </summary>
        public ICompositionSetManager CompositionSetManager => _CompositionSetManager;

        //-------------------------------------------------------------------------------------------------
        //
        //  Setting Properties
        //
        //-------------------------------------------------------------------------------------------------
        public ObservableProjectCollection Projects => _Projects;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirstTime
        {
            get => _AppEnv.IsFirstTime;
            set
            {
                _AppEnv.IsFirstTime = value;
                RaiseUpdated(nameof(IsFirstTime));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WorkingDirectory
        {
            get => _AppEnv.WorkingDirectory;
            set
            {
                _AppEnv.WorkingDirectory = value;
                RaiseUpdated(nameof(WorkingDirectory));
            }
        }

        public ICompositionSet CurrentProject
        {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICompositionSetStore CurrentProjectInfo
        {
            get => _AppEnv.CurrentProject;
            set
            {
                _AppEnv.CurrentProject = value;
                RaiseUpdated(nameof(CurrentProjectInfo));
            }
        }
    }
}
