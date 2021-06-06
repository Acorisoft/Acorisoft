using System;
using System.Reactive;
using System.Threading.Tasks;
using Acorisoft.Studio.ProjectSystem;
using MediatR;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="IComposeSetSystem"/> 接口表示一个抽象的创作集系统接口，用于为应用程序提供创作集新建、打开、关闭等支持。
    /// </summary>
    public interface IComposeSetSystem : IDisposable, IComposeSetFileSystem, IComposeSetPropertySystem, IComposeSetFileSystem2 , IAutoSaveSystem, IComposeSetRequestQueue
    {
        
        /// <summary>
        /// 在一个异步请求中打开一个项目。
        /// </summary>
        /// <param name="project">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task OpenAsync(IComposeProject project);

        
        /// <summary>
        /// 在一个异步请求中打开一个项目。
        /// </summary>
        /// <param name="composeSet">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task OpenAsync(IComposeSet composeSet);

        /// <summary>
        /// 在一个异步请求中关闭一个项目。
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task CloseAsync();

        /// <summary>
        /// 在一个异步请求中新建一个项目。
        /// </summary>
        /// <param name="project">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task NewAsync(INewItemInfo<IComposeSetProperty> project);

        /// <summary>
        /// 获取当前创作集系统的一个数据流，当前数据流用于表示创作集系统是否已经打开。
        /// </summary>
        IObservable<bool> IsOpen { get; }

        /// <summary>
        /// 获取当前创作集系统的一个数据流，当前数据流用于表示创作集系统打开的创作集。
        /// </summary>
        IObservable<IComposeSet> ComposeSet { get; }

        /// <summary>
        /// 获取当前创作集系统集成的中介者。
        /// </summary>
        IMediator Mediator { get; }
    }
}