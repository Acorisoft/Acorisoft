using System;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    /// <summary>
    /// <see cref="NavigateToViewEventArgs"/> 事件类型用于描述一次导航所需要的事件参数。
    /// </summary>
    public class NavigateToViewEventArgs : EventArgs
    {
        public NavigateToViewEventArgs(IPageViewModel lastViewModel, IPageViewModel currentViewModel)
        {
            Last = lastViewModel;
            Current = currentViewModel;
        }

        /// <summary>
        /// 获取导航之前的上一次视图模型。
        /// </summary>
        public IPageViewModel Last { get; }
        
        /// <summary>
        /// 获取当前导航的视图模型。
        /// </summary>
        public IPageViewModel Current { get; }
    }

    public sealed class IxContentChangedEventArgs : EventArgs
    {
        public IxContentChangedEventArgs(IQuickViewModel qv, IQuickViewModel tv, IQuickViewModel cv, IQuickViewModel ev)
        {
            QuickView = qv;
            ToolView = tv;
            ContextualView = cv;
            ExtraView = ev;
        }
        public IQuickViewModel QuickView { get; }
        public IQuickViewModel ToolView { get; }
        public IQuickViewModel ContextualView { get; }
        public IQuickViewModel ExtraView { get; }
    }

    public class DialogShowingEventArgs : EventArgs
    {
        public  IDialogViewModel ViewModel { get; }
    }
}