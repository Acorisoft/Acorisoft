using System;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Dialogs;
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
        internal DialogShowingEventArgs(DefaultDialogContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ViewModel = context.ViewModel ?? throw new ArgumentNullException(nameof(context));
        }
        internal DefaultDialogContext Context { get; }
        public IDialogViewModel ViewModel { get; }
    }
    
    public class WizardShowingEventArgs: EventArgs
    {
        internal WizardShowingEventArgs(StackedDialogContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ViewModel = context.ViewModel ?? throw new ArgumentNullException(nameof(context));
        }
        internal StackedDialogContext Context { get; }
        public IDialogViewModel ViewModel { get; }
    }

    public class DialogChangedEventArgs : EventArgs
    {
        internal DialogChangedEventArgs(IDialogViewModel viewModel)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }
        public IDialogViewModel ViewModel { get; }
    }

    public class PromptShowingEventArgs : EventArgs
    {
        internal PromptShowingEventArgs(PromptDialogContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ViewModel = context.ViewModel ?? throw new ArgumentNullException(nameof(context));
        }
        internal PromptDialogContext Context { get; }
        public IDialogViewModel ViewModel { get; }
    }
}