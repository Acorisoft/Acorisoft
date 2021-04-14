using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class ActivatingContext<TDataSet, TProperty> : IActivatingContext<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        public ActivatingContext(TDataSet set, ILoadContext context)
        {
            Name = context?.Name;
            Directory = context?.Directory;
            FileName = context?.FileName;
            Activating = set;
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
