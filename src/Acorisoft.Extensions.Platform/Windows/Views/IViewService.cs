using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    public interface IViewService
    {
        bool CanGoBack { get; }

        /// <summary>
        /// 设置显示的视图。
        /// </summary>
        /// <param name="vm">传递要显示的视图所关联的视图模型。</param>
        void NavigateTo(IPageViewModel vm);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        void NavigateTo(ISplashViewModel vm);
        
        /// <summary>
        /// 执行一个耗时的操作，并且显示等待界面避免界面被用户操作。
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task Waiting(Action operation, string description);

        Task Waiting(IEnumerable<Tuple<Action, string>> operations);

        /// <summary>
        /// 当导航事件发生时触发该事件。
        /// </summary>
        event NavigateToViewEventHandler Navigating;
        event IsBusyEventHandler IsBusy;
        event BusyStateChangedEventHandler BusyStateChanged;
    }
}