using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Services
{
    internal interface IDialogEventRaiser
    {
        void Close();
        void Update(IDialogViewModel content);
    }

    public partial class ViewService : IDialogEventRaiser, IDialogService
    {
        private IDisposable _dialogDo, _dialogDci, _dialogDce;
        private Subject<Unit> _dialogOpening;
        private Subject<Unit> _dialogClosing;
        private Subject<object> _dialogChanged;
        private Stack<IDialogContext> _dialogContextStack;

        private class CommandImpl : ICommand
        {
            private Action _execute;
            private Func<bool> _canExecute;

            public CommandImpl(Action execute,Func<bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return _canExecute();
            }

            public void Execute(object parameter)
            {
                _execute();
            }
        }

        private void InitializeDialog()
        {
            _dialogChanged = new Subject<object>();
            _dialogClosing = new Subject<Unit>();
            _dialogOpening = new Subject<Unit>();
            _dialogContextStack = new Stack<IDialogContext>();
        }

        private void DisposeDialog()
        {
            _dialogDci?.Dispose();
            _dialogDce?.Dispose();
            _dialogDo?.Dispose();
        }

        private IDialogContext GetCurrentContext() => _dialogContextStack.Count > 0 ? _dialogContextStack.Peek() : null;

        void IDialogEventRaiser.Close()
        {
            _dialogClosing.OnNext(Unit.Default);
            _dialogContextStack.Pop();
        }

        void IDialogEventRaiser.Update(IDialogViewModel content)
        {
            _dialogChanged.OnNext(content);
        }

        public void SetDialog(IDialogHostCore dialogHostCore)
        {
            if (dialogHostCore == null)
            {
                return;
            }

            DisposeDialog();


            var newInstance = dialogHostCore ?? throw new ArgumentNullException(nameof(dialogHostCore));

            _dialogDo = newInstance.SubscribeDialogOpening(_dialogOpening);
            _dialogDci = newInstance.SubscribeDialogClosing(_dialogClosing);
            _dialogDce = newInstance.SubscribeDialogChanged(_dialogChanged);
        }

        /// <summary>
        /// 指示当前对话框上下文是否能够完成操作或者执行下一步操作。
        /// </summary>
        public bool CanNextOrComplete()
        {
            var rawContext = GetCurrentContext();

            if (rawContext is IStackedDialogContext sdc)
            {
                //
                // 判断是否可以下一步
                return sdc.Count > sdc.CurrentIndex && sdc.VerifyAccess();
            }

            //
            // 判断是否可以完成
            // ReSharper disable once PatternAlwaysOfType
            return rawContext is IDialogContext context && context.VerifyAccess();
        }

        public bool CanIgnoreOrSkip()
        {
            //
            // 判断是否可以完成
            // ReSharper disable once PatternAlwaysOfType
            return GetCurrentContext() is IStackedDialogContext context && context.VerifyAccess() &&
                   context.Count > context.CurrentIndex;
        }

        public bool CanCancel()
        {
            //
            // 指示是否可以取消。
            // ReSharper disable once PatternAlwaysOfType
            return GetCurrentContext() is IDialogContext context && context.CanCancel();
        }

        public bool CanLast()
        {
            //
            // 判断是否可以完成
            // ReSharper disable once PatternAlwaysOfType
            return GetCurrentContext() is IStackedDialogContext context && context.CanLast() &&
                   context.Count > context.CurrentIndex;
        }

        public void IgnoreOrSkip()
        {
            (GetCurrentContext() as WizardDialogContext)?.IgnoreOrSkip();
        }

        public void Last()
        {
            (GetCurrentContext() as WizardDialogContext)?.Last();
        }

        public void Cancel()
        {
            GetCurrentContext()?.CanCancel();
        }

        public void NextOrComplete()
        {
            GetCurrentContext()?.NextOrComplete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IDialogSession> ShowDialog(IDialogViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            //
            // 创建上下文
            var context = new NormalDialogContext(viewModel, this);

            //
            // 上下文入栈。
            _dialogContextStack.Push(context);

            //
            // 
            _dialogChanged.OnNext(context.ViewModel);
            _dialogOpening.OnNext(Unit.Default);

            return context.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModels"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IDialogSession> ShowWizard(IEnumerable<IDialogViewModel> viewModels, IViewModel share)
        {
            if (viewModels is null)
            {
                throw new ArgumentNullException(nameof(viewModels));
            }

            if (share is null)
            {
                throw new ArgumentNullException(nameof(share));
            }

            //
            // 创建上下文
            var context = new WizardDialogContext(viewModels, share, this);

            //
            // 上下文入栈。
            _dialogContextStack.Push(context);

            //
            // 
            _dialogChanged.OnNext(context.ViewModel);
            _dialogOpening.OnNext(Unit.Default);

            return context.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<bool?> ShowPrompt(IDialogViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            //
            // 创建上下文
            var context = new PromptDialogContext(viewModel, this);

            //
            // 上下文入栈。
            _dialogContextStack.Push(context);

            //
            // 
            _dialogChanged.OnNext(context.ViewModel);
            _dialogOpening.OnNext(Unit.Default);

            return context.Task;
        }

        /// <summary>
        /// 对话框改变的流。
        /// </summary>
        public IObservable<object> DialogChanged => _dialogChanged;

        /// <summary>
        /// 对话框开始的流。
        /// </summary>
        public IObservable<Unit> DialogOpening => _dialogOpening;


        /// <summary>
        /// 对话框结束的流。
        /// </summary>
        public IObservable<Unit> DialogClosing => _dialogClosing;
    }
}