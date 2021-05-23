using System;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public interface IToastHost
    {
        object Content { get; set; }
    }
    
    public interface IToastHostCore
    {
        IDisposable SubscribeToastPushing(IObservable<IToastViewModel> observable);
    }
}