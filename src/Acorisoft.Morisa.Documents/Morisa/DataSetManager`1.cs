using Acorisoft.Morisa.Internal;
using LiteDB;
using DynamicData;
using DynamicData.Binding;
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
using Acorisoft.Morisa.Core;
using System.IO;

namespace Acorisoft.Morisa
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    public abstract class DataSetManager<TDataSet> where TDataSet : DataSet
    {
        public const string ExternalCollectionName = "Externals";

        private protected readonly DelegateObserver<TDataSet>   DataSetStream;
        private protected readonly DelegateObserver<Resource>   ResourceStream;
        private protected readonly IDisposable                  ResourceDisposable;
        private protected TDataSet DataSet;

        protected DataSetManager()
        {
            DataSetStream = new DelegateObserver<TDataSet>(DataSetChanged);
            ResourceStream = new DelegateObserver<Resource>(ResourceChanged);
            ResourceDisposable = Observable.FromEvent<Resource>(x => ResourceChangedEvent += x, x => ResourceChangedEvent -= x)
                                           .SubscribeOn(ThreadPoolScheduler.Instance)
                                           .Subscribe(x =>
                                           {                                               
                                               ResourceChanged(x);
                                               OnResourceChanged(x);
                                           });
        }

        protected void DataSetChanged(TDataSet set)
        {
            if (set is TDataSet)
            {
                if (DetermineDataSetInitialization(set))
                {
                    InitializeFromDatabase(set);
                }
                else
                {
                    InitializeFromPattern(set);
                }

                OnDataSetChanged(set);
            }
        }

        protected void ResourceChanged(Resource resource)
        {
            if(resource is InDatabaseResource idr)
            {                
                PerformanceInDatabaseResource(idr);
            }
            else if(resource is OutsideResource osr)
            {
                PerformanceOutsideResource(osr);
            }
        }

        protected void PerformanceInDatabaseResource(InDatabaseResource idr)
        {
            if(idr == null)
            {

            }

            if (!File.Exists(idr.FileName))
            {

            }

            idr.Id = Factory.GenereateGuid();
            try
            {
                DataSet.Database
                       .FileStorage
                       .Upload(idr.Id,
                               idr.FileName);
            }
            catch
            {
                // TODO: Handle Exception
            }
        }

        protected void PerformanceOutsideResource(OutsideResource osr)
        {
            if (osr == null)
            {

            }

            if (!File.Exists(osr.FileName))
            {

            }

            osr.Id = Factory.GenereateGuid();
            try
            {
                OnPerformanceOutsideResource(osr);
            }
            catch
            {
                // TODO: Handle Exception
            }
        }

        protected virtual void OnPerformanceOutsideResource(OutsideResource resource)
        {

        }

        protected virtual void OnResourceChanged(Resource resource)
        {

        }

        /// <summary>
        /// 使用数据库中的数据初始化。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected virtual void InitializeFromDatabase(TDataSet set)
        {

        }

        /// <summary>
        /// 数据库未初始化，使用指定的模式来初始化数据库并写入数据。。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected virtual void InitializeFromPattern(TDataSet set)
        {

        }

        /// <summary>
        /// 当设定集发生改变时调用该方法。这是一个PostProcess操作。
        /// </summary>
        /// <param name="set">当前的设定集上下文。</param>
        protected virtual void OnDataSetChanged(TDataSet set)
        {

        }

        /// <summary>
        /// 指示当前的数据库是否初始化。
        /// </summary>
        /// <param name="cs">指定要判断初始化状态的数据库，要求参数不能为。</param>
        protected virtual bool DetermineDataSetInitialization(TDataSet cs)
        {
            return false;
        }


        /// <summary>
        /// 获取当前的输入流。当前输入流是一个数据集。
        /// </summary>

        public IObserver<TDataSet> Input => DataSetStream;


        /// <summary>
        /// 获取一个资源流。
        /// </summary>
        public IObserver<Resource> Resource => ResourceStream;
        internal event Action<Resource> ResourceChangedEvent;
    }
}
