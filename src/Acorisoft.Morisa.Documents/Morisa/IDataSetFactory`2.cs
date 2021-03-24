using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSetFactory{TDataSet, TProperty}"/> 接口用于抽象表示一个数据集工厂，
    /// </summary>
    /// <typeparam name="TDataSet">具体的数据集类型。</typeparam>
    /// <typeparam name="TProperty">具体的数据集属性类型。</typeparam>
    public interface IDataSetFactory<TDataSet, TProperty> : IDataSetFactory<TDataSet>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
        /// <summary>
        /// 从指定的存储上下文中的保存并打开一个数据集。
        /// </summary>
        /// <param name="saveContext">指定要保存的数据集存储上下文。</param>
        void Save(ISaveContext<TProperty> saveContext);

        /// <summary>
        /// 从指定的加载上下文中打开一个数据集。
        /// </summary>
        /// <param name="loadContext">指定要打开的数据集加载上下文。</param>
        void Load(ILoadContext loadContext);

        /// <summary>
        /// 获取一个观测数据集属性更新的观测流。
        /// </summary>
        IObservable<TProperty> PropertyStream { get; }
    }
}
