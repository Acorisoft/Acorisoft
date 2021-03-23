using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSetFactory{TDataSet}"/> 接口用于抽象表示一个泛型数据集工厂，为用户提供加载数据集、观测数据集更新支持。
    /// </summary>
    /// <typeparam name="TDataSet">具体的数据集类型。</typeparam>
    public interface IDataSetFactory<TDataSet> : IDataSetFactory
        where TDataSet : IDataSet
    {
        /// <summary>
        /// 获取一个观测数据集更新的数据流。
        /// </summary>
        IObservable<TDataSet> DataSetStream { get; }
    }
}
