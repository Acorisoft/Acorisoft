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
    public class SaveContext : ISaveContext
    {
        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 存储上下文所存储的上下文名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 存储上下文所存储的作者名称。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 存储上下文所存储的摘要。
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 存储上下文所存储的主题。
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public string FileName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public class SaveContext<TProperty> : SaveContext, ISaveContext<TProperty> where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public TProperty Property { get; set; }
    }
}
