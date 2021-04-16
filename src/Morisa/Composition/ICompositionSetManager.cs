using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Core;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="ICompositionSetManager"/> 接口用于表示一个抽象的创作集管理器接口，用于为用户提供创作集的加载、关闭以及提供访问创作集相关功能支持。
    /// </summary>
    public interface ICompositionSetManager : IDataSetManager<CompositionSet, CompositionSetProperty>, IDisposable
    {
        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        void Switch(ICompositionSetContext context);

        /// <summary>
        /// 获取当前创作集管理器的中介者。
        /// </summary>
        ICompositionSetMediator Mediator { get; }


        /// <summary>
        /// 获取当前正在活动的创作集 <see cref="ICompositionSetContext"/> 实例。
        /// </summary>
        IActivatingContext<CompositionSet, CompositionSetProperty> Activating { get; }

        /// <summary>
        /// 获取当前所有已经打开的创作集 <see cref="ICompositionSetContext"/> 实例。
        /// </summary>
        ReadOnlyObservableCollection<ICompositionSetContext> Opening { get; }

        /// <summary>
        /// 获取当前创作集管理器的释放状态。
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器关闭新的创作集时触发。
        /// </summary>
        event CompositionClosedHandler Closed;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器打开新的创作集时触发。
        /// </summary>
        event CompositionOpenedHandler Opened;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器切换到新的创作集时触发。
        /// </summary>
        event CompositionSwitchHandler Switched;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器释放资源时触发。
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler PropertyChanged;
    }
}
