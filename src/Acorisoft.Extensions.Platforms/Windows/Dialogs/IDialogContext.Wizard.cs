using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Dialogs
{
    internal class WizardDialogContext : DialogContextBase, IStackedDialogContext
    {
        private readonly Stack<IDialogViewModel> _nextStack;
        private readonly Stack<IDialogViewModel> _lastStack;

        public WizardDialogContext(IEnumerable<IDialogViewModel> viewModels,IViewModel share, IDialogEventRaiser raiser) : base(raiser)
        {
            TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
            _nextStack = new Stack<IDialogViewModel>();
            _lastStack = new Stack<IDialogViewModel>();
            Share = share ?? throw new ArgumentNullException(nameof(share));

            foreach (var viewModel in viewModels.Reverse())
            {
                if (viewModel is null)
                {
                    continue;
                }
                _nextStack.Push(viewModel);
                Count++;
            }

            Session = new DialogSession();
            ViewModel = _nextStack.Pop();
            Raiser.Update(ViewModel);
        }

        public bool CanLast()
        {
            return _lastStack.Count > 0;
        }

        public void Last()
        {
            _nextStack.Push(ViewModel);
            ViewModel = _lastStack.Pop();
            Raiser.Update(ViewModel);
        }

        public override void NextOrComplete()
        {
            if (Count > CurrentIndex)
            {
                //
                // 下一步
                _lastStack.Push(ViewModel);
                ViewModel = _nextStack.Pop();
                Raiser.Update(ViewModel);
            }
            else
            {
                _nextStack.Clear();
                _lastStack.Clear();
                ViewModel = null;
                //
                // 完成
                Session.SetIsCompleted(true);
                Session.SetResult(Share);
                TaskCompletionSource.SetResult(Session);
                Raiser.Close();
            }
        }

        public void IgnoreOrSkip()
        {
            if (Count > CurrentIndex)
            {
                //
                // 下一步
                _lastStack.Push(ViewModel);
                ViewModel = _nextStack.Pop();
                Raiser.Update(ViewModel);
            }
            else
            {
                _nextStack.Clear();
                _lastStack.Clear();
                ViewModel = null;
                //
                // 完成
                Session.SetIsCompleted(true);
                Session.SetResult(Share);
                TaskCompletionSource.SetResult(Session);
                Raiser.Close();
            }
        }

        public override void Cancel()
        {
            
            _nextStack.Clear();
            _lastStack.Clear();
            ViewModel = null;
            //
            // 完成
            Session.SetIsCompleted(false);
            Session.SetResult(Share);
            TaskCompletionSource.SetResult(Session);
            Raiser.Close();
        }

        public int Count { get; }

        public int CurrentIndex { get; }

        public IViewModel Share { get; }
        internal DialogSession Session { get; }
        internal TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }
        internal Task<IDialogSession> Task => TaskCompletionSource.Task;
    }
}