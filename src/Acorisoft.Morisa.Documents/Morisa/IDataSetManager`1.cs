using System;

namespace Acorisoft.Morisa
{
    public interface IDataSetManager<TDataSet> : IDisposable
        where TDataSet : DataSet, IDataSet
    {


        /// <summary>
        /// 获取一个观测当前数据集工厂是否已经加载数据集的数据流。该数据流用于表示当前数据集工厂是否已经加载了数据集。
        /// </summary>
        IObservable<bool> IsOpenStream { get; }

        /// <summary>
        /// 
        /// </summary>
        IObservable<TDataSet> DataSetStream { get; }

        /// <summary>
        /// 获取当前工作中的数据集。
        /// </summary>
        TDataSet DataSet { get; }
    }
}