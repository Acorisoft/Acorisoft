using Acorisoft.Morisa.Internal;
using System;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
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
using Acorisoft.Morisa.Core;
using System.Diagnostics.Contracts;

namespace Acorisoft.Morisa
{
    public abstract class DataSetManager<TDataSet, TProfile> : DataSetManager<TDataSet>, IDataSetManager<TDataSet, TProfile> 
        where TDataSet : DataSet<TProfile>
        where TProfile : class, IProfile
    {
        private protected readonly DelegateObserver<TProfile> ProfileStream;
        private protected readonly IDisposable ProfileDisposable;
        private TProfile _Profile;

        public DataSetManager() : base()
        {
            ProfileStream = new DelegateObserver<TProfile>(ProfileChanged);
            ProfileDisposable = Observable.FromEvent<TProfile>(x => OnProfileChanged += x, x => OnProfileChanged -= x)
                                          .SubscribeOn(ThreadPoolScheduler.Instance)
                                          .Subscribe(x =>
                                          {
                                              if (x.Cover != null)
                                              {
                                                  Resource.OnNext(x.Cover);
                                              }
                                              Singleton(x);
                                              ProfileChangedCore(x);
                                          });
        }

        protected void SetGenerateContext(TProfile setting)
        {
            _Profile = setting;
        }


        /// <summary>
        /// 在数据库中查询或者创建一个新的单例。
        /// </summary>
        /// <typeparam name="T">要查询的单例对象类型。</typeparam>
        /// <returns>返回一个已经存在于数据库中的单例实例或者创建一个新的单例实例并保存于数据库当中。</returns>
        protected internal T Singleton<T>()
        {
            var key = typeof(T).FullName;
            T instance;
            if (DataSet.DB_External.Exists(Query.EQ("_id", key)))
            {
                instance = DatabaseMixins.Deserialize<T>(DataSet.DB_External.FindById(key));
            }
            else
            {
                instance = ClassActivator.CreateInstance<T>();
                DataSet.DB_External.Upsert(key, DatabaseMixins.Serialize(instance));
            }

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        protected internal T Singleton<T>(T instance)
        {
            DataSet.DB_External.Upsert(typeof(TProfile).FullName, DatabaseMixins.Serialize(instance));
            return instance;
        }

        /// <summary>
        /// 在数据库中查询或者创建一个新的单例。
        /// </summary>
        /// <typeparam name="T">要查询的单例对象类型。</typeparam>
        /// <param name="database">数据库</param>
        /// <returns>返回一个已经存在于数据库中的单例实例或者创建一个新的单例实例并保存于数据库当中。</returns>
        protected internal static T Singleton<T>(LiteDatabase database)
        {
            var key = typeof(T).FullName;
            var collection = database.GetCollection(ExternalCollectionName);
            T instance;
            if (collection.Exists(Query.EQ("_id", key)))
            {
                instance = DatabaseMixins.Deserialize<T>(collection.FindById(key));
            }
            else
            {
                instance = ClassActivator.CreateInstance<T>();
                collection.Upsert(key, DatabaseMixins.Serialize(instance));
            }

            return instance;
        }

        /// <summary>
        /// 当配置信息改变的时候。
        /// </summary>
        /// <param name="profile">新的配置信息。</param>
        protected void ProfileChanged(TProfile profile)
        {
            if (profile.Cover != null)
            {
                Contract.Assert(DataSet != null);
                Contract.Assert(DataSet.Setting != null);

                //
                // Determined previous version
                if (DataSet.Setting.Cover is InDatabaseResource)
                {
                    DataSet.Database.FileStorage.Delete(DataSet.Setting.Cover.Id);
                }

                if (profile.Cover != null)
                {
                    Resource.OnNext(profile.Cover);
                }
            }

            OnProfileChanged?.Invoke(profile);
        }

        protected override sealed void DataSetChanged(TDataSet set)
        {
            base.DataSetChanged(set);
        }

        /// <summary>
        /// 当配置信息改变的时候。
        /// </summary>
        /// <param name="profile">新的配置信息。</param>
        protected virtual void ProfileChangedCore(TProfile profile)
        {

        }

        /// <summary>
        /// 使用数据库中的数据初始化。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected override void InitializeFromDatabase(TDataSet set)
        {
            set.DB_External = set.Database.GetCollection(ExternalCollectionName);

            //
            // 创建新的配置信息。
            set.Setting = Singleton<TProfile>();

        }

        /// <summary>
        /// 数据库未初始化，使用指定的模式来初始化数据库并写入数据。。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected override void InitializeFromPattern(TDataSet set)
        {
            set.Setting = Singleton(CreateProfileCore());
        }

        /// <summary>
        /// 创建新的配置信息。在派生类中重写该方法以便生成针对性的配置信息。
        /// </summary>
        /// <returns>返回一个配置信息实例。</returns>
        protected TProfile CreateProfileCore()
        {
            if(_Profile == null)
            {
                return default;
            }

            return _Profile;
        }

        public IObserver<TProfile> Profile => ProfileStream;
        public event Action<TProfile> OnProfileChanged;
    }
}
