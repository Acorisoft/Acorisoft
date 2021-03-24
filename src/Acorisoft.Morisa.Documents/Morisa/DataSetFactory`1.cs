using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSetFactory<TDataSet> : DataSetFactory, IDataSetFactory<TDataSet>
        where TDataSet : DataSet, IDataSet
    {
        protected readonly Subject<TDataSet> ProtectedDataSetStream;

        protected DataSetFactory()
        {
            ProtectedDataSetStream = new Subject<TDataSet>();
        }
        /// <summary>
        /// 在数据库中查询或者创建一个新的单例。
        /// </summary>
        /// <typeparam name="T">要查询的单例对象类型。</typeparam>
        /// <returns>返回一个已经存在于数据库中的单例实例或者创建一个新的单例实例并保存于数据库当中。</returns>
        protected internal static T Singleton<T>(TDataSet ds)
        {
            var key = typeof(T).FullName;
            T instance;
            if (ds.DB_External.Exists(Query.EQ("_id", key)))
            {
                instance = DatabaseMixins.Deserialize<T>(ds.DB_External.FindById(key));
            }
            else
            {
                instance = ClassActivator.CreateInstance<T>();
                ds.DB_External.Upsert(key, DatabaseMixins.Serialize(instance));
            }

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        protected internal static T Singleton<T>(TDataSet ds, T instance)
        {
            ds.DB_External.Upsert(typeof(T).FullName, DatabaseMixins.Serialize(instance));
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<TDataSet> DataSetStream => ProtectedDataSetStream;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<bool> IsOpenStream => ProtectedIsOpenStream;

        /// <summary>
        /// 
        /// </summary>
        public TDataSet DataSet { get; private protected set; }

    }
}
