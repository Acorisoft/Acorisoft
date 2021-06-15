using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Acorisoft.Studio.Core
{
    /// <summary>
    /// <see cref="IComposeSetPropertySystem"/> 接口表示一个抽象的创作集属性系统接口，用于实现创作集属性设置的支持。
    /// </summary>
    public interface IComposeSetPropertySystem
    {
        /// <summary>
        /// 在一个异步请求中开启对创作集属性的修改行为。
        /// </summary>
        /// <param name="property">指定要修改的属性。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task SetPropertyAsync(IComposeSetProperty property);

        /// <summary>
        /// 在一个异步请求中获取创作集属性。
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task<IComposeSetProperty> GetPropertyAsync();
        
        /// <summary>
        /// 获取当前创作集属性系统的一个数据流，当前数据流用于表示创作集属性系统中新的属性更新通知。
        /// </summary>
        IObservable<IComposeSetProperty> Property { get; }
    }
}