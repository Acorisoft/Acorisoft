using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSet{TProperty}"/> 接口用于表示一个抽象的带数据集属性的数据集接口，用于提供对数据集属性操作的支持。
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public interface IDataSet<TProperty> : IDataSet where TProperty : DataSetProperty, IDataSetProperty
    {
        /// <summary>
        /// 获取应用与当前数据集的数据集属性。
        /// </summary>
        TProperty Property { get; }
    }
}
