using Acorisoft.Properties;
using DynamicData;
using LiteDB;
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
    public abstract class DataSetManager<TDataSet, TProperty> : DataSetManager<TDataSet>, IDataSetManager<TDataSet>
        where TDataSet : DataSet, IDataSet
        where TProperty : DataSetProperty, IDataSetProperty
    {
        protected readonly SourceList<TDataSet> EditableDataSetCollection;
        private protected readonly ReadOnlyObservableCollection<TDataSet> BindableDataSetCollection;
        private protected readonly Subject<TProperty> ProtectedPropertyStream;
        private TProperty _Property;

        protected DataSetManager()
        {
            EditableDataSetCollection = new SourceList<TDataSet>();
            EditableDataSetCollection.Connect()
                              .Bind(out BindableDataSetCollection)
                              .Subscribe(x =>
                              {

                              });
        }

        public void Load(ILoadContext context)
        {
            if (context == null)
            {
                throw new InvalidOperationException(string.Format(SR.LoadContext_Invalid, SR.LoadContext_Null));
            }

            if (!File.Exists(context.FileName))
            {
                throw new InvalidOperationException(string.Format(SR.LoadContext_Invalid, SR.LoadContext_FileName_Null));
            }

            //
            // 在派生类中操作，以决定如何加载数据集。
            OnLoad(context);
        }

        protected void OnDataSetChanged(TDataSet oldDataSet, TDataSet newDataSet)
        {
            if(oldDataSet is not null)
            {
                //
                // Clear State
                oldDataSet.Dispose();
            }

            if(newDataSet is not null)
            {
                //
                // Assign data set
                DataSet = newDataSet;

                //
                //
                if (DetermineDatabaseInitialization(newDataSet))
                {
                    InitializeFromDatabase(newDataSet);
                }
                else
                {
                    InitializeFromCode(newDataSet);
                }

                //
                // notification
                ProtectedDataSetStream.OnNext(newDataSet);

                //
                //
                EditableDataSetCollection.Add(newDataSet);
            }

            ProtectedIsOpenStream.OnNext(newDataSet is not null);
        }

        /// <summary>
        /// 重写该方法，实现从数据集中初始化支持。
        /// </summary>
        /// <param name="ds"></param>
        protected virtual void InitializeFromDatabase(TDataSet ds)
        {
            _Property = Singleton<TProperty>(ds);
        }

        /// <summary>
        /// 重写该方法，实现从代码中初始化数据集。
        /// </summary>
        /// <param name="ds"></param>
        protected virtual void InitializeFromCode(TDataSet ds)
        {
            _Property = Singleton(ds, CreatePropertyCore());
        }

        protected abstract TProperty CreatePropertyCore();

        /// <summary>
        /// 重写该方法，用来判断当前数据库是否初始化。
        /// </summary>
        /// <param name="set">指定要判断数据库初始化的数据集。</param>
        /// <returns>返回一个值，该值用于表示当前数据集的初始化状态，如果已初始化则返回true，否则返回false。</returns>
        protected virtual bool DetermineDatabaseInitialization(TDataSet set)
        {
            return false;
        }

        protected abstract void OnLoad(ILoadContext context);

        /// <summary>
        /// 
        /// </summary>
        public TProperty Property => _Property;

        /// <summary>
        /// 获取当前数据集管理器所管理的数据集集合。
        /// </summary>
        public ReadOnlyObservableCollection<TDataSet> DataSets => BindableDataSetCollection;

        /// <summary>
        /// 获取当前数据集管理器所管理的数据集集合。
        /// </summary>
        public IObservable<TProperty> PropertyStream => ProtectedPropertyStream;
    }
}
