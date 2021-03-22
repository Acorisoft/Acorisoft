using Acorisoft.Morisa.Core;
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

namespace Acorisoft.Morisa.v2
{
    public interface IDataSetManager<TDataSet> where TDataSet : DataSet
    {
        /// <summary>
        /// 
        /// </summary>
        IObservable<bool> IsOpen { get; }

        /// <summary>
        /// 获取当前的输入流。当前输入流是一个数据集。
        /// </summary>
        public IObserver<TDataSet> Input { get; }


        /// <summary>
        /// 获取一个资源流。
        /// </summary>
        public IObserver<Resource> Resource { get; }
    }
}
