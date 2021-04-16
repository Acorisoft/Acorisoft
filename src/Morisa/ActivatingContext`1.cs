using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class ActivatingContext<TDataSet, TProperty> : IActivatingContext<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="context"></param>
        public ActivatingContext(TDataSet set, ILoadContext context)
        {
            Name = context?.Name;
            Directory = context?.Directory;
            FileName = context?.FileName;
            Activating = set ?? throw new ArgumentNullException(nameof(set));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="context"></param>
        public ActivatingContext(TDataSet set, ISaveContext context)
        {
            Name = context?.Name;
            Directory = context?.Directory;
            FileName = context?.FileName;
            Activating = set ?? throw new ArgumentNullException(nameof(set));
        }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文名称。
        /// </summary>
        public TDataSet Activating { get; }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文名称。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// 获取当前 <see cref="IActivatingContext{TDataSet, TProperty}"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public string FileName { get; }
    }
}
