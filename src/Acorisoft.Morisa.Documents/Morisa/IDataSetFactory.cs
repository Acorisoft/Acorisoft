using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSetFactory"/> 接口用于表示一个抽象的数据集工厂接口，用于为用户提供数据集操作支持。
    /// </summary>
    public interface IDataSetFactory : IDisposable
    {
        /// <summary>
        /// 获取一个观测当前数据集工厂是否打开的数据流。
        /// </summary>
        IObservable<bool> IsOpenStream { get; }

        /// <summary>
        /// 获取一个观测当前数据集工厂资源存储完毕事件的数据流。
        /// </summary>
        IObservable<Resource> CompletedStream { get; }

        /// <summary>
        /// 获取一个观测当前数据集工厂引发异常的数据流。
        /// </summary>
        IObservable<Exception> ExceptionStream { get; }
    }
}
