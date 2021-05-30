using System;
using System.Reactive;

namespace Acorisoft.Studio.ProjectSystem
{
    /// <summary>
    /// <see cref="ICompositionSetRequestQueue"/> 接口表示的是文档请求队列，用于完成用户界面
    /// </summary>
    public interface ICompositionSetRequestQueue
    {
        void Set();
        void Unset();
        IObservable<Unit> Requesting { get; }
        IObservable<Unit> Responding { get; }
    }
}