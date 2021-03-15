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
using DryIoc;
using Acorisoft.Morisa.ViewModels;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------

        public const string ConnectionString = "FileName=MapTools.Morisa-Setting;Mode=Shared;Initial Size=4MB";
        public const string ExternalCollectionName = "Externals";
        public const string SettingName = "Morisa.App.Setting";

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        private string _Title;
        private IApplicationEnvironment             _AppEnv;
        private LiteCollection<BsonDocument>        _DB_Externals;
        private IDialogManager                      _DialogManager;

        private readonly LiteDatabase               _AppDB;
        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public AppViewModel(IContainer container)
        {
            _AppDB = new LiteDatabase(ConnectionString);
            _Title = "设定集";
            OnInitialize();
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
        /// 获取或设置当前应用的标题。
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        //-------------------------------------------------------------------------------------------------
        //
        //  Setting Properties
        //
        //-------------------------------------------------------------------------------------------------


    }
}
