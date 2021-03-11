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
using Splat;
using Acorisoft.Morisa.Dialogs;
using System.Windows;

using ProjectInfoCollection = DynamicData.Binding.ObservableCollectionExtended<Acorisoft.Morisa.IMorisaProjectInfo>;
using BsonDocumentDBCollection = LiteDB.ILiteCollection<LiteDB.BsonDocument>;
using ProjectInfoDBCollection = LiteDB.ILiteCollection<Acorisoft.Morisa.IMorisaProjectInfo>;
using EntityServices = System.Collections.Generic.IEnumerable<Acorisoft.Morisa.Core.IEntityService>;

namespace Acorisoft.Morisa.ViewModels
{

#pragma warning disable CA1816,CA1822

    public class AppViewModel : ReactiveObject, IRoutableViewModel
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------
        public const string ExternalsCollectionName                 = "Externals";
        public const string ProjectInfoCollectionName               = "Projects";
        public const string SettingObjectName                       = "Morisa.Setting";
        public const string AppDBConnectionString                   = "FileName=Acorisoft.Morisa.Morisa-Setting;Initial Size= 4MB;Connection=Shared";

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

        protected class Setting
        {
            public bool IsFirstTime { get; set; }

            [BsonField("info")]
            public IMorisaProjectInfo CurrentProjectInfo { get; set; }
            public string ProjectFolder { get; set; }
            public bool IgnoreFileDuplicate { get; set; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly CompositeDisposable        _Disposable;
        private readonly LiteDatabase               _AppDB;
        private readonly IDialogManager             _DialogManager;
        private readonly IMorisaProjectManager      _ProjectManager;
        private readonly EntityServices             _EntityServices;
        private readonly IMorisaFileManager         _FileManager;

        //
        // Collection
        private ProjectInfoCollection               _ProjectCollection;
        private Setting                             _Setting;

        //
        // DB Collection
        private BsonDocumentDBCollection            _DB_Externals;
        private ProjectInfoDBCollection             _DB_Projects;

        private string                  _Title;
        private IScreen                 _Screen;
        private IMorisaProjectInfo      _CurrentProjectInfo;
        private IMorisaProject          _CurrentProject;


        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        public AppViewModel(IMorisaProjectManager projectMgr , EntityServices entitySrves , IDialogManager dialogMgr,IMorisaFileManager fileMgr)
        {

            _Disposable = new CompositeDisposable();
            _DialogManager = dialogMgr;
            _ProjectManager = projectMgr;
            _EntityServices = entitySrves;
            _FileManager = fileMgr;

            //
            // when project manager load an new project 
            // it will return the project info back to the vm
            // and we can set the current project to database
            projectMgr.ProjectInfo
                      .Subscribe(OnProjectInfoChanged)
                      .DisposeWith(_Disposable);

            //
            // when project manager load an new project
            // it will update all entity service
            projectMgr.Project
                      .SubscribeOn(RxApp.TaskpoolScheduler)
                      .Subscribe(OnProjectChanged)
                      .DisposeWith(_Disposable);


            try
            { 
                //
                // 初始化应用数据库
                _AppDB = new LiteDatabase(AppDBConnectionString);
            }
            catch(Exception ex)
            {
                //
                // 提示错误发生
                MessageBox.Show(ex.Message);

                //
                // 退出应用
                Application.Current.Shutdown();
            }
            //
            // 
            _Disposable.Add(_AppDB);

            OnInitialize();
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Methods
        //
        //-------------------------------------------------------------------------------------------------

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

            //
            // 
            _CurrentProjectInfo = _Setting.CurrentProjectInfo;

            //
            //
            if(_CurrentProjectInfo != null)
            {
                _ProjectManager.LoadOrCreateProject(_CurrentProjectInfo);
            }
        }

        protected void OnProjectChanged(IMorisaProject value)
        {
            CurrentProject = value;

            //
            // 通知新的项目
            //
            // IMorisaFileManager 用于为项目提供文件写入支持，但是项目之间是解耦合的
            // 我们要针对性的为它赋值
            _FileManager.Project.OnNext(value);
            _FileManager.Project.OnCompleted();

            //
            // 通知实体服务
            if(_EntityServices != null)
            {
                foreach(var service in _EntityServices)
                {
                    if(service.Project == null)
                    {
                        continue;
                    }

                    service.Project.OnNext(value);
                    service.Project.OnCompleted();
                }
            }
        }

        protected void OnProjectInfoChanged(IMorisaProjectInfo value)
        {
            CurrentProjectInfo = value;
            UpdateSetting();

            //
            //
            if (!_ProjectCollection.Contains(value))
            {
                _ProjectCollection.Add(value);
                UpdateProjectCollection();
            }
        }

        protected Setting CreateInstanceCore()
        {
            return new Setting
            {
                IsFirstTime = true ,
                CurrentProjectInfo = null
            };
        }

        void UpdateProjectCollection()
        {
            _DB_Projects.Upsert(_ProjectCollection);
        }

        public void UpdateSetting()
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

        /// <summary>
        /// 获取或设置当前的第一次运行证据。
        /// </summary>
        public bool IsFirstTime
        {
            get => _Setting.IsFirstTime;
            set
            {
                _Setting.IsFirstTime = value;
                this.RaisePropertyChanged(nameof(IsFirstTime));
                UpdateSetting();
            }
        }

        /// <summary>
        /// 获取或设置当前的项目目录.
        /// </summary>
        public string ProjectFolder
        {
            get => _Setting.ProjectFolder;
            set
            {
                _Setting.ProjectFolder = value;
                this.RaisePropertyChanged(nameof(ProjectFolder));
                UpdateSetting();
            }
        }

        /// <summary>
        /// 获取或设置当前的项目.
        /// </summary>
        public IMorisaProject CurrentProject
        {
            get => _CurrentProject;
            set
            {
                _CurrentProject = value;
            }
        }

        /// <summary>
        /// 是否忽略文件冲突
        /// </summary>
        public bool IgnoreFileDuplicate
        {
            get => _Setting.IgnoreFileDuplicate;
            set
            {
                _Setting.IgnoreFileDuplicate = value;
                _FileManager.IgnoreFileDuplicate = value;
                this.RaisePropertyChanged(nameof(IgnoreFileDuplicate));
            }
        }

        /// <summary>
        /// 获取或设置当前的项目信息。
        /// </summary>
        public IMorisaProjectInfo CurrentProjectInfo
        {
            get => _CurrentProjectInfo;
            set
            {
                if(!EqualityComparer<IMorisaProjectInfo>.Default.Equals(value,_CurrentProjectInfo))
                {
                    //
                    //
                    _CurrentProjectInfo = value;
                    _Setting.CurrentProjectInfo = value;

                    //
                    //
                    this.RaisePropertyChanged(nameof(CurrentProjectInfo));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UrlPathSegment { get => "app"; }

        /// <summary>
        /// 获取当前应用程序界别的 <see cref="IScreen"/> 接口。
        /// </summary>
        public IScreen HostScreen
        {
            get
            {
                if(_Screen == null)
                {
                    _Screen = Locator.Current.GetService<IScreen>();
                }

                return _Screen;
            }
        }

        /// <summary>
        /// 获取或设置应用程序标题
        /// </summary>
        public string Title
        {
            get => _Title;
            set => this.RaiseAndSetIfChanged(ref _Title , value);
        }

        /// <summary>
        /// 
        /// </summary>
        public IDialogManager DialogManager => _DialogManager;

        /// <summary>
        /// 
        /// </summary>
        public IMorisaProjectManager ProjectManager => _ProjectManager;

        /// <summary>
        /// 
        /// </summary>
        public IMorisaFileManager FileManager => _FileManager;
    }
}
