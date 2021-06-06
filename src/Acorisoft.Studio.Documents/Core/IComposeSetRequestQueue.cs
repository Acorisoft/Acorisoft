using System;
using System.Reactive;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="IComposeSetRequestQueue"/> 接口表示一个抽象的创作集请求队列接口，用于实现应用程序间操作同步支持。
    /// </summary>
    public interface IComposeSetRequestQueue
    {
        /// <summary>
        /// 设置一个请求。
        /// </summary>
        void Set();
        
        /// <summary>
        /// 取消一个请求
        /// </summary>
        void Unset();
        
        /// <summary>
        /// 清空所有请求。
        /// </summary>
        void Clear();
        
        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在打开。
        /// </summary>
        IObservable<Unit> Responding { get; }
        
        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在关闭。
        /// </summary>
        IObservable<Unit> Requesting { get; }
    }
}