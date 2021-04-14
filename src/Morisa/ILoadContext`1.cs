using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="ILoadContext"/> 接口表示一个抽象的加载上下文。加载上下文用于为数据集管理器、数据集工厂提供打开加载过的数据上下文支持。
    /// </summary>
    /// <typeparam name="T"><see cref="ILoadContext"/> 加载上下文的具体加载类型。</typeparam>
    public interface ILoadContext
    {
        /// <summary>
        /// 获取当前 <see cref="ILoadContext"/> 加载上下文所存储的上下文名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取当前 <see cref="ILoadContext"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        string Directory { get; }


        /// <summary>
        /// 获取当前 <see cref="ILoadContext"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        string FileName { get; }
    }

    /// <summary>
    /// <see cref="ILoadContext{T}"/> 接口表示一个抽象的加载上下文。加载上下文用于为数据集管理器、数据集工厂提供打开加载过的数据上下文支持。
    /// </summary>
    /// <typeparam name="T"><see cref="ILoadContext{T}"/> 加载上下文的具体加载类型。</typeparam>
    public interface ILoadContext<T> : ILoadContext
    {
        /// <summary>
        /// 获取当前 <see cref="ILoadContext{T}"/> 加载上下文所存储的上下文的存储上下文。
        /// </summary>
        T Context { get; }
    }
}
