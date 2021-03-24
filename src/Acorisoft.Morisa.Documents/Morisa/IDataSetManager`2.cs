using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IDataSetManager<TDataSet, TProperty> : IDataSetManager<TDataSet>
        where TDataSet : DataSet, IDataSet
        where TProperty : DataSetProperty, IDataSetProperty
    {
        /// <summary>
        /// 从指定的加载上下文中打开一个数据集。
        /// </summary>
        /// <param name="loadContext">指定要打开的数据集加载上下文。</param>
        void Load(ILoadContext loadContext);

        /// <summary>
        /// 
        /// </summary>
        TProperty Property { get; }

        /// <summary>
        /// 获取一个观测数据集属性更新的观测流。
        /// </summary>
        IObservable<TProperty> PropertyStream { get; }


        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<TDataSet> DataSets { get; }
    }
}
