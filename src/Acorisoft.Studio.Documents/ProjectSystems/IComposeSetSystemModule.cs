using System;
using MediatR;

namespace Acorisoft.Studio.ProjectSystems
{
    public interface IComposeSetSystemModule : INotificationHandler<ComposeSetCloseInstruction>,
        INotificationHandler<ComposeSetOpenInstruction>, INotificationHandler<ComposeSetSaveInstruction>
    {
        /// <summary>
        /// 获取当前创作集系统模块的一个数据流，当前数据流用于表示创作集系统模块是否已经打开。
        /// </summary>
        IObservable<bool> IsOpen { get; }
    }
}