using System;
using System.Reactive;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public interface IActivityIndicator
    {
        /// <summary>
        /// 设置当前 <see cref="IActivityIndicator"/> 的繁忙状态。
        /// </summary>
        bool IsBusy { get; set; }

        /// <summary>
        /// 获取或设置当前 <see cref="IActivityIndicator"/> 实例对于繁忙状态的描述。
        /// </summary>
        string Description { get; set; }
    }

    public interface IActivityIndicatorCore
    {
        /// <summary>
        /// 订阅繁忙状态改变的事件流。
        /// </summary>
        IDisposable SubscribeBusyStateChanged(IObservable<string> observable);

        /// <summary>
        /// 订阅繁忙状态开始的事件流。
        /// </summary>
        IDisposable SubscribeBusyStateBegin(IObservable<Unit> observable);

        /// <summary>
        /// 订阅繁忙状态结束的事件流。
        /// </summary>
        IDisposable SubscribeBusyStateEnd(IObservable<Unit> observable);
    }
}