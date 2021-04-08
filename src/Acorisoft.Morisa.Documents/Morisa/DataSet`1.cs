using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataSet{TProperty}"/> 类型实例表示一个具备数据集属性的数据集基类，用于为用于提供数据集操作的支持。
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class DataSet<TProperty> : DataSet, IDataSet<TProperty> where TProperty : DataSetProperty, IDataSetProperty
    {
        /// <summary>
        /// 获取当前数据集的资源存储模式。
        /// </summary>
        public abstract ResourceMode ResourceMode { get; }

        /// <summary>
        /// 获取应用与当前数据集的数据集属性。
        /// </summary>
        public TProperty Property { get; set; }
    }
}
