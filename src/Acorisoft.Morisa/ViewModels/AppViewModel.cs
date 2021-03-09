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
using ProjectInfoCollection = DynamicData.Binding.ObservableCollectionExtended<Acorisoft.Morisa.IMorisaProjectInfo>;
using BsonDocumentDBCollection = LiteDB.ILiteCollection<LiteDB.BsonDocument>;
using ProjectInfoDBCollection = LiteDB.ILiteCollection<Acorisoft.Morisa.IMorisaProjectInfo>;

namespace Acorisoft.Morisa.ViewModels
{
#pragma warning disable CA1816,CA1822
    public class AppViewModel : ReactiveObject
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------
        public const string ExternalsCollectionName                 = "Externals";
        public const string ProjectInfoCollectionName               = "Projects";
        public const string SettingObjectName                       = "Morisa.Setting";
        public const string AppDBConnectionString                   = "FileName=App.Morisa-Setting;Initial Size= 4MB;mode=share";

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

        protected class Setting
        {
            public bool IsFirstTime { get; set; }
            public IMorisaProjectInfo CurrentProject { get; set; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly CompositeDisposable        _Disposable;
        private readonly LiteDatabase               _AppDB;

        //
        // Collection
        private ProjectInfoCollection               _ProjectCollection;
        private Setting                             _Setting;

        //
        // DB Collection
        private BsonDocumentDBCollection            _DB_Externals;
        private ProjectInfoDBCollection             _DB_Projects;


        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        public AppViewModel(IMorisaProjectManager projectMgr , IEnumerable<IEntityService> entitySrves)
        {

            _Disposable = new CompositeDisposable();

            //
            // when project manager load an new project 
            // it will return the project info back to the vm
            // and we can set the current project to database
            projectMgr.ProjectInfo
                      .Subscribe(x => CurrentProject = x)
                      .DisposeWith(_Disposable);

            ////
            //// when project manager load an new project
            //// it will update all entity service
            //projectMgr.Project
            //          .SubscribeOn(RxApp.TaskpoolScheduler)
            //          .Subscribe(x =>
            //          {
            //              foreach (var srv in entitySrves)
            //              {
            //                  var observer = srv.ProjectObserver;
            //                  observer.OnNext(x);
            //                  observer.OnCompleted();
            //              }
            //          })
            //          .DisposeWith(_Disposable);


            //
            // 初始化应用数据库
            _AppDB = new LiteDatabase(AppDBConnectionString);

            //
            // 
            _Disposable.Add(_AppDB);

            OnInitialize();
        }

        protected void OnInitialize()
        {
            _AppDB.Exists(ExternalsCollectionName)

                  //
                  // Initialize Setting
                  .ToFactory<Setting , BsonDocument>(x => CreateInstanceCore())
                  .WithCollectionName(ExternalsCollectionName)
                  .WithFieldName(SettingObjectName)
                  .ToField(ref _Setting)
                  .ToLiteCollection(ref _DB_Externals)

                  //
                  // Initialize ProjectCollection

                  .ToFactory<IMorisaProjectInfo , ProjectInfoCollection>(x => new ProjectInfoCollection() , x => new ProjectInfoCollection(x.FindAll().ToArray()))
                  .WithCollectionName(ProjectInfoCollectionName)
                  .ToCollection(ref _ProjectCollection)
                  .ToLiteCollection(ref _DB_Projects);
        }

        protected Setting CreateInstanceCore()
        {
            return new Setting
            {
                IsFirstTime = false ,
                CurrentProject = null
            };
        }

        void UpdateProjectCollection()
        {
            _DB_Projects.Upsert(new List<IMorisaProjectInfo>());
        }

        void UpdateSetting()
        {
            //
            var document = BsonMapper.Global.Serialize(_Setting).AsDocument;

            //
            _DB_Externals.Upsert(SettingObjectName , document);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        public bool IsFirstTime {
            get => _Setting.IsFirstTime;
            set {
                _Setting.IsFirstTime = value;
                this.RaisePropertyChanged(nameof(IsFirstTime));
                UpdateSetting();
            }
        }

        public IMorisaProjectInfo CurrentProject { get; set; }
    }
}
