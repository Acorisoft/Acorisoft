using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading;
using System.Reactive.Disposables;
using DynamicData.Binding;
using Acorisoft.Morisa.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Runtime.CompilerServices;

namespace Acorisoft.Morisa.ViewModels
{
    public class SettingViewModel : ReactiveObject
    {
        //---------------------------------------------------------------------------------------
        //
        // Internal Classes
        //
        //---------------------------------------------------------------------------------------

        protected class Setting
        {
            [BsonId]
            public string Id { get; set; }
            public ProjectInfo CurrentProject { get; set; }
            public bool IsFirstTime { get; set; }
        }

        //---------------------------------------------------------------------------------------
        //
        // Constants
        //
        //---------------------------------------------------------------------------------------

        public const string ExternalCollectionName = "Externals";
        public const string SettingId = "Acorisoft.Morisa.Setting";
        public const string ProjectModelCollectionName = "Projects";

        //---------------------------------------------------------------------------------------
        //
        // Variants
        //
        //---------------------------------------------------------------------------------------


        private readonly ILiteDatabase      _appDB;
        private readonly Setting            _setting;
        private ProjectInfo                 _CurrentProject;
        private bool                        _IsFirstTime;

        //---------------------------------------------------------------------------------------
        //
        // Variants
        //
        //---------------------------------------------------------------------------------------


        private readonly ILiteCollection<BsonDocument>             _externals;
        private ObservableCollectionExtended<ProjectInfo>          _Projects;

        //---------------------------------------------------------------------------------------
        //
        // Methods
        //
        //---------------------------------------------------------------------------------------

        public SettingViewModel(ILiteDatabase appDB)
        {

            if (!appDB.CollectionExists(ExternalCollectionName))
            {
                //
                // 如果集合不存在，则表示需要初始化内容
                _externals = appDB.GetCollection<BsonDocument>(ExternalCollectionName);

                //
                // 初始化设置
                _setting = CreateInstanceCore();

                //
                // 开始事务
                appDB.BeginTrans();

                //
                // 插入新的设置
                _externals.Insert(BsonMapper.Global.Serialize(_setting).AsDocument);

                //
                // 提交事务
                appDB.Commit();
            }
            else
            {
                //
                // 获取集合
                _externals = appDB.GetCollection<BsonDocument>(ExternalCollectionName);

                //
                // 反序列化设置
                _setting = BsonMapper.Global.Deserialize<Setting>(_externals.FindOne(Query.EQ("_id" , SettingId)));
            }

            _appDB = appDB ?? throw new ArgumentNullException(nameof(appDB));
            //
            // 初始化其他部分的内容
            Initialize();
        }

        protected void Initialize()
        {
            var projectCollection = _appDB.GetCollection<ProjectInfo>(ProjectModelCollectionName);

            _CurrentProject = _setting.CurrentProject;
            _IsFirstTime = _setting.IsFirstTime;

            //
            // 初始化项目集合
            _Projects = new ObservableCollectionExtended<ProjectInfo>(projectCollection.FindAll().ToArray());
            _Projects.ObserveCollectionChanges()
                     .ObserveOn(RxApp.MainThreadScheduler)
                     .Subscribe(x => OnProjectCollectionChanged(x.EventArgs));

            this.WhenAnyValue(x => x.CurrentProject)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(OnCurrentProjectChanged);

        }

        protected void OnProjectCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            RxApp.TaskpoolScheduler.Schedule(x =>
            {
                _appDB.BeginTrans();
                _appDB.GetCollection<ProjectInfo>(ProjectModelCollectionName)
                      .Upsert(_Projects);
                _appDB.Commit();
            });
        }

        protected void OnCurrentProjectChanged(ProjectInfo project)
        {
            _setting.CurrentProject = CurrentProject;
            OnSettingChanged();
        }

        protected void OnSettingChanged()
        {
            _appDB.BeginTrans();
            _externals.Upsert(BsonMapper.Global.Serialize(_setting).AsDocument);
            _appDB.Commit();
        }

        protected static Setting CreateInstanceCore()
        {
            return new Setting
            {
                Id = SettingId,
                IsFirstTime = true,
                CurrentProject = null
            };
        }

        protected void SetValueAndRaise<T>(ref T backend,T value,[CallerMemberName]string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backend , value))
            {
                this.RaisePropertyChanging(name);
                backend = value;
                this.RaisePropertyChanged(name);
                OnSettingChanged();
            }
        }

        //---------------------------------------------------------------------------------------
        //
        // Properties
        //
        //---------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public ProjectInfo CurrentProject {
            get => _CurrentProject;
            set {
                _setting.CurrentProject = value;
                SetValueAndRaise(ref _CurrentProject , value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirstTime {
            get => _IsFirstTime;
            set {
                _setting.IsFirstTime = value;
                SetValueAndRaise(ref _IsFirstTime , value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollectionExtended<ProjectInfo> Projects => _Projects;
    }
}
