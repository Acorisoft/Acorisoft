using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Services
{
    public interface IViewService
    {
        #region ForceBusyState

        /// <summary>
        /// 强制程序进入繁忙状态。
        /// </summary>
        /// <remarks>
        /// <para>如果一个操作必须使得程序中所有部件都等待操作结束了，才能完成状态同步，那么你可以考虑调用这个方法来实现。</para>
        /// <para>这个操作如果被调用了多次，那么结束时间将会以最后一次为准。</para>
        /// </remarks>
        /// <param name="operation">需要执行的操作。</param>
        /// <exception cref="ArgumentNullException">如果传入的参数为空时则将会引发该异常。</exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>返回一个可等待的任务。</returns>
        Task ForceBusyState(ObservableOperation operation);

        /// <summary>
        /// 强制程序进入繁忙状态。
        /// </summary>
        /// <param name="operations">需要执行的操作。</param>
        /// <remarks>
        /// <para>如果一个操作必须使得程序中所有部件都等待操作结束了，才能完成状态同步，那么你可以考虑调用这个方法来实现。</para>
        /// <para>这个操作如果被调用了多次，那么结束时间将会以最后一次为准。</para>
        /// </remarks>
        /// <returns>返回一个可等待的任务。</returns>
        Task ForceBusyState(IEnumerable<ObservableOperation> operations);

        /// <summary>
        /// 设置默认的繁忙状态指示器。
        /// </summary>
        /// <param name="indicator">默认的繁忙状态指示器，实例要求不能为空。</param>
        /// <exception cref="ArgumentNullException">传递的参数为空时引发该异常。</exception>
        void SetBusyIndicator(IBusyIndicatorCore indicator);

        void ManualStartBusyState(string description);

        void ManualEndBusyState();
        
        /// <summary>
        /// 繁忙状态的流。
        /// </summary>
        IObservable<string> BusyStateChanged { get; }
        
        /// <summary>
        /// 控制繁忙状态开始的流。
        /// </summary>
        IObservable<Unit> BusyStateBegin { get; }
        
        
        /// <summary>
        /// 控制繁忙状态结束的流。
        /// </summary>
        IObservable<Unit> BusyStateEnd { get; }

        #endregion

        #region Dialog
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogHostCore"></param>
        void SetDialog(IDialogHostCore dialogHostCore);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<IDialogSession> ShowDialog(IDialogViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModels"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        Task<IDialogSession> ShowWizard(IEnumerable<IDialogViewModel> viewModels, IViewModel share);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<bool?> ShowPrompt(IDialogViewModel viewModel);
        
        /// <summary>
        /// 对话框改变的流。
        /// </summary>
        IObservable<object> DialogChanged { get; }
        
        /// <summary>
        /// 对话框开始的流。
        /// </summary>
        IObservable<Unit> DialogOpening { get; }
        
        
        /// <summary>
        /// 对话框结束的流。
        /// </summary>
        IObservable<Unit> DialogClosing { get; }

        #endregion

        #region Toast

        void Toast(string content);
        void Toast(string content, object icon);
        void Toast(string content, object icon, TimeSpan duration);
        
        void SetToast(IToastHostCore dialogHostCore);

        #endregion

        #region ViewAware
        void NavigateTo(IPageViewModel page);

        void NavigateTo(IQuickViewModel quickView, IQuickViewModel toolView, IQuickViewModel contextView,
            IQuickViewModel extraView);

        
        IObservable<IPageViewModel> Page { get; }
        IObservable<IQuickViewModel> QuickView { get; }
        IObservable<IQuickViewModel> ToolView { get; }
        IObservable<IQuickViewModel> ContextView { get; }
        IObservable<IQuickViewModel> ExtraView { get; }
        #endregion
    }
}