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

using BsonDocumentDBCollection = LiteDB.ILiteCollection<LiteDB.BsonDocument>;
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
        private readonly EntityServices             _EntityServices;

        //
        // Collection
        private Setting                             _Setting;

        //
        // DB Collection
        private BsonDocumentDBCollection            _DB_Externals;

        private string                  _Title;
        private IScreen                 _Screen;


        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        public AppViewModel(EntityServices entitySrves , IDialogManager dialogMgr)
        {

            _Disposable = new CompositeDisposable();
            _DialogManager = dialogMgr;
            _EntityServices = entitySrves;



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

        }

        

        protected Setting CreateInstanceCore()
        {
            return new Setting
            {
                IsFirstTime = true ,
            };
        }

        void UpdateProjectCollection()
        {
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

        public bool IgnoreFileDuplicate
        {
            get => _Setting.IgnoreFileDuplicate;
            set {
                _Setting.IgnoreFileDuplicate = value;
                this.RaisePropertyChanged(nameof(IgnoreFileDuplicate));
            }
        }
    }
}
