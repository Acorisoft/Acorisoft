using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSetFactory"/> 接口用于抽象表示一个数据集工厂，为用户操作提供资源操作、分页等基础的操作功能。
    /// </summary>
    public interface IDataSetFactory : IDisposable
    {
        
        /// <summary>
        /// 获取一个观测当前数据集工厂是否已经加载数据集的数据流。该数据流用于表示当前数据集工厂是否已经加载了数据集。
        /// </summary>
        IObservable<bool> IsOpen { get; }

        /// <summary>
        /// 获取一个观测分页请求操作的数据流。
        /// </summary>
        /// <remarks>
        /// 这个流用于为数据集工厂提供通知分页请求变化支持。
        /// </remarks>
        IObserver<IPageRequest> Page { get; }

        /// <summary>
        /// 获取一个观测资源操作完成的数据流。
        /// </summary>
        /// <remarks>
        /// 这个流用于在同步线程中通知资源的变化。
        /// </remarks>
        IObservable<Resource> Completed { get; }

        /// <summary>
        /// 获取一个资源流的观测者，用于观测资源的变化，以便在不同线程中异步添加资源。
        /// </summary>
        IObserver<Resource> Resource { get; }
    }
}
