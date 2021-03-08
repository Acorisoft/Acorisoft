using LiteDB;
using System;
using System.Collections.Generic;
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

namespace Acorisoft.Morisa.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        protected class Setting
        {
            [BsonId]
            public string Id { get; set; }
        }

        public const string ExternalCollectionName = "Externals";
        public const string SettingId = "Acorisoft.Morisa.Setting";

        private readonly ILiteDatabase _appDB;
        private readonly Setting _setting;
        private readonly ObservableCollectionExtended<ProjectModel> _Projects;


        public SettingViewModel(ILiteDatabase appDB)
        {
            ILiteCollection<BsonDocument> externals;

            if (!appDB.CollectionExists(ExternalCollectionName))
            {
                //
                // 如果集合不存在，则表示需要初始化内容
                externals = appDB.GetCollection<BsonDocument>(ExternalCollectionName);

                //
                // 初始化设置
                _setting = InitializeSetting();

                //
                // 开始事务
                appDB.BeginTrans();

                //
                // 插入新的设置
                externals.Insert(BsonMapper.Global.Serialize<Setting>(_setting).AsDocument);

                //
                // 提交事务
                appDB.Commit();
            }
            else
            {
                externals = appDB.GetCollection<BsonDocument>(ExternalCollectionName);
                _setting = BsonMapper.Global.Deserialize<Setting>(externals.FindOne(Query.EQ("_id" , SettingId)));
            }

            _appDB = appDB ?? throw new ArgumentNullException(nameof(appDB));

            //
            // 初始化项目集合
            _Projects = new ObservableCollectionExtended<ProjectModel>(
                appDB.GetCollection<ProjectModel>()
                     .FindAll()
                     .ToArray());

            _Projects.WhenAnyPropertyChanged()
                     .ObserveOn(RxApp.MainThreadScheduler)
                     .SubscribeOn(RxApp.MainThreadScheduler)
                     .Subscribe(OnProjectCollectionChanged);
        }

        protected void OnProjectCollectionChanged(ObservableCollectionExtended<ProjectModel> collection)
        {

        }

        protected static Setting InitializeSetting()
        {
            return new Setting
            {
                Id = SettingId
            };
        }

        public ObservableCollectionExtended<ProjectModel> Projects => _Projects;
    }
}
