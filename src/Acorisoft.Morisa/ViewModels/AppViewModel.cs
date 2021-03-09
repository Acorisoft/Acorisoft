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
using BsonDocumentCollection = LiteDB.ILiteCollection<LiteDB.BsonDocument>;
namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        public const string ExternalsCollectionName                 = "Externals";
        public const string ProjectInfoCollectionName               = "Projects";
        public const string SettingObjectName                       = "Morisa.Setting";

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

        private readonly CompositeDisposable    _Disposable;
        private readonly LiteDatabase           _AppDB;
        private ProjectInfoCollection           _ProjectCollection;
        private Setting                         _Setting;
        private BsonDocumentCollection          _DB_Externals;
        private ILiteCollection<IMorisaProjectInfo> _DB_Projects;
        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        public AppViewModel(IMorisaProjectManager projectMgr , IEnumerable<IEntityService> entitySrves)
        {

            _Disposable = new CompositeDisposable();
            ////
            //// when project manager load an new project 
            //// it will return the project info back to the vm
            //// and we can set the current project to database
            //projectMgr.ProjectInfo
            //          .Subscribe(x => CurrentProject = x)
            //          .DisposeWith(_Disposable);

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
            _AppDB = new LiteDatabase(@"FileName=App.Morisa-Setting;Initial Size= 4MB;mode=share");

            //
            // 
            _Disposable.Add(_AppDB);

            OnInitialize();
        }

        protected void OnInitialize()
        {
            _DB_Externals = _AppDB.GetCollection<BsonDocument>(ExternalsCollectionName);
            _DB_Projects = _AppDB.GetCollection<IMorisaProjectInfo>(ProjectInfoCollectionName);

            if (!_AppDB.CollectionExists(ExternalsCollectionName))
            {
                _Setting = new Setting
                {
                    IsFirstTime = false ,
                    CurrentProject = null
                };
                _ProjectCollection = new ProjectInfoCollection();
                UpdateProjectCollection();
                UpdateSetting();
            }
            else
            {
                _ProjectCollection = new ProjectInfoCollection(
                    _DB_Projects.FindAll()
                                .ToArray());
                _Setting = _DB_Externals.FindOne<Setting>(SettingObjectName);
            }
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
        public IMorisaProjectInfo CurrentProject { get; set; }
    }
}
