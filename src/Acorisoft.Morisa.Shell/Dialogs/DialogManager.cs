using Acorisoft.Properties;
using Acorisoft.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Dialogs
{
    class DialogManager : IDialogManager
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        protected internal class DialogSession : IDialogSession
        {
            private object _Result;

            protected internal void SetResult(object result)
            {
                _Result = result;
            }

            public T GetResult<T>()
            {
                if(_Result is T genericResult)
                {
                    return genericResult;
                }

                return default;
            }

            public object Result => _Result;
            public bool IsCompleted { get; internal set; }
        }

        protected internal abstract class DisplayContext
        {

            /// <summary>
            /// 
            /// </summary>
            public IViewModel ViewModel { get; protected set; }
        }

        protected internal sealed class DialogDisplayContext : DisplayContext
        {

            public DialogDisplayContext(IViewModel vm)
            {
                ViewModel = vm;
                TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
                Session = new DialogSession();
            }

            /// <summary>
            /// 
            /// </summary>
            public TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }

            /// <summary>
            /// 
            /// </summary>
            public Task<IDialogSession> Task => TaskCompletionSource.Task;

            /// <summary>
            /// 
            /// </summary>
            public DialogSession Session { get; }
        }

        protected internal sealed class ConfirmDisplayContext : DisplayContext
        {

            public ConfirmDisplayContext(IViewModel vm)
            {
                ViewModel = vm;
                TaskCompletionSource = new TaskCompletionSource<bool>();
            }

            /// <summary>
            /// 
            /// </summary>
            public TaskCompletionSource<bool> TaskCompletionSource { get; }

            /// <summary>
            /// 
            /// </summary>
            public Task<bool> Task => TaskCompletionSource.Task;
        }

        protected internal sealed class NotificationDisplayContext : DisplayContext
        {

            public NotificationDisplayContext(IViewModel vm)
            {
                ViewModel = vm;
                TaskCompletionSource = new TaskCompletionSource();
            }

            /// <summary>
            /// 
            /// </summary>
            public TaskCompletionSource TaskCompletionSource { get; }

            /// <summary>
            /// 
            /// </summary>
            public Task Task => TaskCompletionSource.Task;
        }

        protected internal sealed class StepDialogContext : DisplayContext
        {
            private readonly Stack<IViewModel> _LastStack; // last
            private readonly Stack<IViewModel> _NextStack; // next

            public StepDialogContext(IEnumerable<IStepViewModel> StepViewModels, IViewModel Context)
            {
                _NextStack = new Stack<IViewModel>();
                _LastStack = new Stack<IViewModel>();

                foreach (var vm in StepViewModels)
                {
                    vm.Transfer(Context);
                    _NextStack.Push(vm);
                }

                //
                // peek 
                ViewModel = _NextStack.Pop();

                //
                // 初始化 TCS
                TaskCompletionSource = new TaskCompletionSource<IDialogSession>();

                //
                // 初始化 Session
                Session = new DialogSession();

                //
                // 设置Context
                this.Context = Context;
            }

            internal void Next()
            {
                //
                // next : last
                // 3 2  : 1
                _LastStack.Push(ViewModel);
                ViewModel = _NextStack.Pop();
            }

            internal void Last()
            {
                //
                // next  : last
                // 3 2 1 : 
                _NextStack.Push(ViewModel);
                ViewModel = _LastStack.Pop();
            }

            public int Count => _NextStack.Count;

            public IViewModel Context { get; }

            public bool CanNext { 
                get
                {
                    return _NextStack.Count > 0 && ViewModel is IStepViewModel svm && svm.CanNext();
                }
            }

            public bool CanLast
            {
                get
                {
                    return _LastStack.Count > 0 && ViewModel is IStepViewModel svm && svm.CanLast();
                }
            }

            public bool CanIgnore
            {
                get
                {
                    return _NextStack.Count > 0 && ViewModel is IStepViewModel svm && svm.CanNext();
                }
            }


            /// <summary>
            /// 
            /// </summary>
            public TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }

            /// <summary>
            /// 
            /// </summary>
            public Task<IDialogSession> Task => TaskCompletionSource.Task;

            /// <summary>
            /// 
            /// </summary>
            public DialogSession Session { get; }
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        private readonly IFullLogger                _Logger;
        private readonly Stack<DisplayContext>      _Stack;
        private DisplayContext                      _Context;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public DialogManager(ILogManager manager)
        {
            _Logger = manager.GetLogger(this.GetType());
            _Stack = new Stack<DisplayContext>();
        }

        protected static IStepViewModel GetStepViewModel<T>() where T : IStepViewModel
        {
            return Locator.Current.GetService<T>();
        }

        protected static IViewModel GetViewModel<T>() where T : IViewModel
        {
            return Locator.Current.GetService<T>();
        }

        protected internal DisplayContext Pop() => _Stack.Pop();

        protected internal void Resume()
        {
            _Context = _Stack.Peek();

            //
            // 通知对话框改变。
            DialogChanged?.Invoke(_Context);
        }

        #region Confirm


        public Task<bool> Confirm(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Confirm(string title, object content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Confirm<TViewModel>(string title) where TViewModel : IViewModel
        {
            throw new NotImplementedException();
        }

        #endregion Confirm

        #region Notification

        public Task Notification(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task Notification(string title, object content)
        {
            throw new NotImplementedException();
        }

        public Task Notification<TViewModel>(string title) where TViewModel : IViewModel
        {
            throw new NotImplementedException();
        }

        #endregion Notification

        #region Dialog

        public Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IViewModel
        {
            if (Locator.CurrentMutable.HasRegistration(typeof(TViewModel)))
            {
                return Dialog(Locator.Current.GetService<TViewModel>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Dialog_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Dialog_NotRegister);
            }
        }

        public Task<IDialogSession> Dialog(IViewModel vm)
        {
            if (vm is not null)
            {
                var context = new DialogDisplayContext(vm);
                _Context = context;
                _Stack.Push(_Context);
                return context.Task;
            }
            else
            {
                _Logger.Error(SR.DialogManager_Dialog_Null);
                throw new InvalidOperationException(SR.DialogManager_Dialog_Null);
            }
        }

        #endregion Dialog

        #region Step ViewModels

        public Task<IDialogSession> Step<TContext, TStep1, TStep2>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
                GetStepViewModel<TStep4>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
                GetStepViewModel<TStep4>(),
                GetStepViewModel<TStep5>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
                GetStepViewModel<TStep4>(),
                GetStepViewModel<TStep5>(),
                GetStepViewModel<TStep6>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
                GetStepViewModel<TStep4>(),
                GetStepViewModel<TStep5>(),
                GetStepViewModel<TStep6>(),
                GetStepViewModel<TStep7>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
            where TStep8 : IStepViewModel
        {
            var ViewModels = new []
            {
                GetStepViewModel<TStep1>(),
                GetStepViewModel<TStep2>(),
                GetStepViewModel<TStep3>(),
                GetStepViewModel<TStep4>(),
                GetStepViewModel<TStep5>(),
                GetStepViewModel<TStep6>(),
                GetStepViewModel<TStep7>(),
                GetStepViewModel<TStep8>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TContext>(IEnumerable<IStepViewModel> stepViewModels)
            where TContext : IViewModel
        {
            if (stepViewModels is not null && stepViewModels.Count() > 1)
            {
                return Step(stepViewModels, GetViewModel<TContext>());
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step(IEnumerable<IStepViewModel> stepViewModels, IViewModel Context)
        {
            if (stepViewModels is not null && stepViewModels.Count() > 1)
            {
                var context = new StepDialogContext(stepViewModels.Reverse(), Context);

                _Stack.Push(context);

                //
                // 创建上下文
                _Context = context;

                DialogChanged?.Invoke(context);


                return context.Task;
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        #endregion Step ViewModels

        /// <summary>
        /// 
        /// </summary>
        public event Action<DisplayContext> DialogChanged;

        /// <summary>
        /// 
        /// </summary>
        protected internal int Count => _Stack.Count;

        /// <summary>
        /// 
        /// </summary>
        protected internal IFullLogger Logger => _Logger;
    }
}
