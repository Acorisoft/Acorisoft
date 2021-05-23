using System;
using System.Reactive;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public interface IDialogHost
    {
        bool IsOpen { get; set; }
        object Dialog { get; set; }
    }
    
    public interface IDialogHostCore
    {
        IDisposable SubscribeDialogOpening(IObservable<Unit> observable);
        IDisposable SubscribeDialogClosing(IObservable<Unit> observable);
        IDisposable SubscribeDialogChanged(IObservable<object> observable);
    }
}