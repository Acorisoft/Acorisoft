using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IActivatingContext{TDataSet, TProperty}"/> 表示一个活跃数据上下文
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IActivatingContext<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文名称。
        /// </summary>
        TDataSet Activating { get; }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        string Directory { get; }


        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        string FileName { get; }
    }
}
