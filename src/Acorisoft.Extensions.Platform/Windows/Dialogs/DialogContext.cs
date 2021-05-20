using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows.Dialogs
{
    internal abstract class DialogContext
    {
        public virtual void NextOrComplete()
        {
        }

        public virtual void Last()
        {
        }

        public virtual void Cancel()
        {
        }

        public virtual void Ignore()
        {
        }

        protected DialogContext(IDialogEventVisitor helper) => EventHelper = helper;

        protected internal IDialogEventVisitor EventHelper { get; }

        public virtual bool CanLast() => false;

        public IDialogViewModel ViewModel { get; protected set; }
    }

    internal class StackedDialogContext : DialogContext
    {
        public StackedDialogContext(IDialogContext context, IDialogEventVisitor helper) : base(helper)
        {
            TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
            Session = new DialogSession();
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _lastStack = new Stack<IDialogViewModel>();
            _nextStack = new Stack<IDialogViewModel>();
            foreach (var viewModel in context.ViewModels.Reverse())
            {
                _nextStack.Push(viewModel);
            }

            ViewModel = _nextStack.Pop();
            EventHelper.RaiseDialogChanged(ViewModel);
        }

        private readonly Stack<IDialogViewModel> _lastStack;
        private readonly Stack<IDialogViewModel> _nextStack;
        
        public sealed override void Cancel()
        {
            Session.SetIsCompleted(false);
            Session.SetResult(ViewModel);
            TaskCompletionSource.SetResult(Session);
            EventHelper.RaiseDialogClose();
        }

        public sealed override void Last()
        {
            if (_lastStack.Count > 0)
            {
                _nextStack.Push(ViewModel);
                ViewModel = _lastStack.Pop();
                EventHelper.RaiseDialogChanged(ViewModel);
            }
        }

        public sealed override void NextOrComplete()
        {
            if (!ViewModel.VerifyAccess())
            {
                return;
            }

            if (_nextStack.Count == 0)
            {
                //
                // completed
                Session.SetIsCompleted(true);
                Session.SetResult(Context.Share);
                TaskCompletionSource.SetResult(Session);
                EventHelper.RaiseDialogClose();
            }
            else
            {
                _lastStack.Push(ViewModel);
                ViewModel = _nextStack.Pop();
                EventHelper.RaiseDialogChanged(ViewModel);
            }
        }

        public sealed override bool CanLast()
        {
            return _lastStack.Count > 0;
        }

        public sealed override void Ignore()
        {
            if (ViewModel.CanIgnore())
            {
                _lastStack.Push(ViewModel);
                ViewModel = _nextStack.Pop();
                EventHelper.RaiseDialogChanged(ViewModel);
            }
        }

        private IDialogContext Context { get; }
        private DialogSession Session { get; }
        private TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }
        public Task<IDialogSession> Task => TaskCompletionSource.Task;
    }

    internal class DialogSession : IDialogSession
    {
        public void SetIsCompleted(bool result)
        {
            IsCompleted = result;
        }

        public void SetResult(object result)
        {
            Result = result;
        }

        public bool IsCompleted { get; private set; }
        public object Result { get; private set; }
    }

    internal class DefaultDialogContext : DialogContext
    {
        public DefaultDialogContext(IDialogViewModel viewModel, IDialogEventVisitor helper) : base(helper)
        {
            TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
            Session = new DialogSession();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public sealed override void Cancel()
        {
            Session.SetIsCompleted(false);
            Session.SetResult(ViewModel);
            TaskCompletionSource.SetResult(Session);
            EventHelper.RaiseDialogClose();
        }

        public sealed override void Last()
        {
            Cancel();
        }

        public sealed override void NextOrComplete()
        {
            if (ViewModel.VerifyAccess())
            {
                Session.SetIsCompleted(true);
                Session.SetResult(ViewModel);
                TaskCompletionSource.SetResult(Session);
                EventHelper.RaiseDialogClose();
            }
        }

        public sealed override bool CanLast()
        {
            return false;
        }

        public sealed override void Ignore()
        {
            Cancel();
        }

        private DialogSession Session { get; }
        private TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }
        public Task<IDialogSession> Task => TaskCompletionSource.Task;
    }

    internal class PromptDialogContext : DialogContext
    {
        public PromptDialogContext(IDialogViewModel viewModel, IDialogEventVisitor helper) : base(helper)
        {
            TaskCompletionSource = new TaskCompletionSource<bool?>();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public sealed override void NextOrComplete()
        {
            if (ViewModel.VerifyAccess())
            {
                //
                // false => 取消
                // true => 确认
                // null => 忽略
                TaskCompletionSource.SetResult(true);
                EventHelper.RaiseDialogClose();
            }
        }

        public sealed override void Ignore()
        {
            if (ViewModel.CanIgnore())
            {
                TaskCompletionSource.SetResult(null);
                EventHelper.RaiseDialogClose();
            }
        }

        public sealed override void Cancel()
        {
            TaskCompletionSource.SetResult(false);
            EventHelper.RaiseDialogClose();
        }

        public sealed override void Last()
        {
            Cancel();
        }


        private TaskCompletionSource<bool?> TaskCompletionSource { get; }
        public Task<bool?> Task => TaskCompletionSource.Task;
    }
}