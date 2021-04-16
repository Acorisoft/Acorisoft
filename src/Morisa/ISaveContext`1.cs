using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="ISaveContext"/> 接口表示一个抽象的存储上下文。存储上下文用于为数据集管理器、数据集工厂、数据工厂提供数据保存向导支持。
    /// </summary>
    public interface ISaveContext
    {
        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 存储上下文所存储的上下文名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 存储上下文所存储的作者名称。
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 存储上下文所存储的摘要。
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 存储上下文所存储的主题。
        /// </summary>
        string Topic { get; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        string Directory { get; }

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        string FileName { get; }

    }

    /// <summary>
    /// <see cref="ISaveContext{T}"/> 接口表示一个抽象的存储上下文。存储上下文用于为数据集管理器、数据集工厂、数据工厂提供数据保存向导支持。
    /// </summary>
    /// <typeparam name="T"><see cref="ISaveContext{T}"/> 存储上下文的具体加载类型。</typeparam>
    public interface ISaveContext<T> : ISaveContext where T : DataProperty, IDataProperty
    {

        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 加载上下文所存储的上下文属性。
        /// </summary>
        T Property { get; }
    }
}
