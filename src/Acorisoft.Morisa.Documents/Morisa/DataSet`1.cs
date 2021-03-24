using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataSet{TProperty}"/> 表示一个支持
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class DataSet<TProperty> : DataSet , IDataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
        /// <summary>
        /// 获取或设置应用于当前数据集的数据集属性设置。
        /// </summary>
        protected internal TProperty Property { get; internal set; }
    }
}
