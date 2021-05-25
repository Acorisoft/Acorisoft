using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediatR;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface ICompositionSetManager : IDisposable
    {
        /// <summary>
        /// 关闭当前正在打开的项目。
        /// </summary>
        void CloseProject();
        
        /// <summary>
        /// 加载一个项目。
        /// </summary>
        /// <param name="project">指示要加载的项目。</param>
        /// <param name="isOpen">指示是否打开。</param>
        void LoadProject(ICompositionProject project, bool isOpen);
        

        /// <summary>
        /// 使用指定的参数创建一个新的创作集并打开。
        /// </summary>
        /// <param name="newProjectInfo">要传递的参数，要求不能为空。</param>
        /// <returns>返回等待此次操作完成的任务。</returns>
        Task NewProject(INewProjectInfo newProjectInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        Task SetProperty(ICompositionSetProperty property);
        
        /// <summary>
        /// 获取当前创作集的请求队列。
        /// </summary>
        ICompositionSetRequestQueue Queue { get; }
        
        /// <summary>
        /// 获取当前用户创建的所有创作集。
        /// </summary>
        ReadOnlyObservableCollection<ICompositionSet> CompositionSets { get; }

        /// <summary>
        /// 
        /// </summary>
        ICompositionSetPropertyManager PropertyManager { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IObservable<bool> IsOpen { get; }

        /// <summary>
        /// 获取当前正在打开的创作集。
        /// </summary>
        IObservable<ICompositionSet> Composition { get; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="ICompositionSet"/> 的属性。
        /// </summary>
        IObservable<ICompositionSetProperty> Property { get; }
        
        /// <summary>
        /// 获取中介者
        /// </summary>
        IMediator Mediator { get; }
    }
}