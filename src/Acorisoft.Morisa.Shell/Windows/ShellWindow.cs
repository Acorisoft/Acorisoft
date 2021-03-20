using Acorisoft.Morisa.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DryIoc;
using Splat;
using Splat.DryIoc;
using System.Diagnostics;

namespace Acorisoft.Morisa.Windows
{
#pragma warning disable CS8632
    public abstract class ShellWindow : Window, IViewFor, IDialogManager
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        protected class DialogSession : IUpdatableSession
        {
            //-------------------------------------------------------------------------------------------------
            //
            //  Constructors
            //
            //-------------------------------------------------------------------------------------------------
            public DialogSession(IRoutableViewModel viewModel)
            {
                ViewModel = viewModel;
            }

            public DialogSession(IRoutableViewModel viewModel, IDialogManager manager)
            {
                ViewModel = viewModel;
                Manager = manager;
            }

            //-------------------------------------------------------------------------------------------------
            //
            //  Public Methods
            //
            //-------------------------------------------------------------------------------------------------
            public Task<IDialogSession> Update<TViewModel>() where TViewModel : IRoutableViewModel
            {
                return Manager.Dialog<TViewModel>();
            }


            public T GetResult<T>() where T : class
            {
                if (ViewModel is IResultable resultable)
                {
                    return resultable.GetResult() as T;
                }
                return ViewModel as T;
            }

            //-------------------------------------------------------------------------------------------------
            //
            //  Properties
            //
            //-------------------------------------------------------------------------------------------------

            /// <summary>
            /// 
            /// </summary>
            protected internal IDialogManager Manager { get; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsCompleted { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public IRoutableViewModel ViewModel { get; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerDisplay("{Content}")]
        protected class DialogDisplayContext
        {
            //-------------------------------------------------------------------------------------------------
            //
            //  Constructors
            //
            //-------------------------------------------------------------------------------------------------
            public DialogDisplayContext(TaskCompletionSource<IDialogSession> tcs, IRoutableViewModel content, IDialogManager manager)
            {
                TaskCompletionSource = tcs;
                Task = tcs.Task;
                Content = content;
                Session = new DialogSession(content, manager);
            }

            public DialogDisplayContext(TaskCompletionSource<IDialogSession> tcs, IRoutableViewModel content, object context, Guid id)
            {
                TaskCompletionSource = tcs;
                Task = tcs.Task;
                Content = content;
                Session = new DialogSession(content);
                StepId = id;
                Context = context;
            }

            //-------------------------------------------------------------------------------------------------
            //
            //  Properties
            //
            //-------------------------------------------------------------------------------------------------

            public Guid? StepId { get; }

            /// <summary>
            /// 
            /// </summary>
            public TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }

            /// <summary>
            /// 
            /// </summary>
            public Task<IDialogSession> Task { get; }

            /// <summary>
            /// 
            /// </summary>
            public IRoutableViewModel Content { get; }

            /// <summary>
            /// 
            /// </summary>
            public DialogSession Session { get; }

            public object Context { get; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Dependency Properties
        //
        //-------------------------------------------------------------------------------------------------
        static ShellWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellWindow), new FrameworkPropertyMetadata(typeof(ShellWindow)));
            DialogPropertyKey = DependencyProperty.RegisterReadOnly(
                "Dialog",
                typeof(object),
                typeof(ShellWindow),
                new PropertyMetadata(null, OnDialogChanged));
            DialogProperty = DialogPropertyKey.DependencyProperty;
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly Stack<DialogDisplayContext> _ContextStack;
        private readonly Stack<DialogDisplayContext> _LastStack;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public ShellWindow()
        {
            //
            // 
            _ContextStack = new Stack<DialogDisplayContext>();
            _LastStack = new Stack<DialogDisplayContext>();

            //
            //
            CommandBindings.Add(new CommandBinding(DialogCommands.Ok, DoDialogOk, CanDialogOk));
            CommandBindings.Add(new CommandBinding(DialogCommands.Cancel, DoDialogCancel, CanDialogCancel));
            CommandBindings.Add(new CommandBinding(DialogCommands.LastStep, DoDialogLastStep, CanDialogLastStep));
            CommandBindings.Add(new CommandBinding(DialogCommands.NextStep, DoDialogNextStep, CanDialogNextStep));
            CommandBindings.Add(new CommandBinding(DialogCommands.Completion, DoDialogCompletion, CanDialogCompletion));
            CommandBindings.Add(new CommandBinding(DialogCommands.Skip, DoDialogSkip, CanDialogSkip));
            CommandBindings.Add(new CommandBinding(WindowCommands.Goto, DoGoto, CanGoto));
            CommandBindings.Add(new CommandBinding(WindowCommands.Goback, DoGoback, CanGoback));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, DoWindowClose, CanWindowClose));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, DoWindowMinimum, CanWindowMinimum));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, DoWindowRestore, CanWindowRestore));

            //
            //
            Locator.CurrentMutable.RegisterConstant<IDialogManager>(this);

            this.Unloaded += OnUnloaded;
            this.Loaded += OnLoaded;
            this.DataContextChanged += OnDataContextChanged;
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= OnUnloaded;
            this.Loaded -= OnLoaded;
            this.DataContextChanged -= OnDataContextChanged;
        }

        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------------------
        //
        //  CommandBindings
        //
        //-------------------------------------------------------------------------------------------------

        protected void DoDialogOk(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ContextStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            var Context = _ContextStack.Pop();

            //
            //
            Context.Session.IsCompleted = true;

            //
            //
            Context.TaskCompletionSource.SetResult(Context.Session);

            //
            //
            if (_ContextStack.Count > 0)
            {
                Context = _ContextStack.Pop();

                if (Context.Content is IStepViewModelContext nextVMContext)
                {
                    nextVMContext.PostContext(Context.Context);
                }

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
            else
            {
                //
                //
                _LastStack.Clear();

                //
                // Fire Event
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogCloseEvent
                });

                //
                //
                ClearValue(DialogPropertyKey);
            }
        }

        protected void DoDialogCancel(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ContextStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            var Context = _ContextStack.Pop();

            //
            //
            Context.Session.IsCompleted = false;

            //
            //
            Context.TaskCompletionSource.SetResult(Context.Session);

            //
            // 将所有步骤退栈
            while (_ContextStack.Count > 0 && Context.StepId != null && _ContextStack.Peek().StepId == Context.StepId)
            {
                Context = _ContextStack.Pop();
            }


            //
            //
            if (_ContextStack.Count > 0)
            {

                Context = _ContextStack.Pop();

                if (Context.Content is IStepViewModelContext nextVMContext)
                {
                    nextVMContext.PostContext(Context.Context);
                }

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
            else
            {
                //
                // Fire Event
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogCloseEvent
                });

                //
                //
                ClearValue(DialogPropertyKey);

                //
                //
                _LastStack.Clear();
            }
        }

        protected void DoDialogNextStep(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ContextStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            //
            // 当前上下文必须在堆栈顶部
            var Context = _ContextStack.Pop();

            //
            // 将当前的上下文退栈，并且将当前上下文存储在上一步堆栈上。
            _LastStack.Push(Context);

            //
            //
            if (_ContextStack.Count > 0)
            {
                if (Context.StepId == _ContextStack.Peek().StepId)
                {
                    Context = _ContextStack.Peek();

                    if (Context.Content is IStepViewModelContext nextVMContext)
                    {
                        nextVMContext.PostContext(Context.Context);
                    }
                    //
                    // Set New Dialog Content To Property Dialog
                    SetValue(DialogPropertyKey, Context.Content);
                }
                else
                {
                    //
                    // Fire Event
                    RaiseEvent(new RoutedEventArgs
                    {
                        RoutedEvent = DialogCloseEvent
                    });

                    //
                    //
                    ClearValue(DialogPropertyKey);

                    //
                    //
                    _LastStack.Clear();


                    //
                    // 完成提前对话框
                    if (Context.Content is IResultable resultable)
                    {
                        Context.Session.IsCompleted = resultable.VerifyModel();
                    }

                    //
                    //
                    Context.TaskCompletionSource.SetResult(Context.Session);
                }
            }
            else
            {

                //
                // Fire Event
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogCloseEvent
                });

                //
                //
                ClearValue(DialogPropertyKey);

                //
                //
                _LastStack.Clear();

                //
                // 完成提前对话框
                if (Context.Content is IResultable resultable)
                {
                    Context.Session.IsCompleted = resultable.VerifyModel();
                }

                //
                //
                Context.TaskCompletionSource.SetResult(Context.Session);
            }
        }

        protected void DoDialogLastStep(object sender, ExecutedRoutedEventArgs e)
        {
            if (_LastStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            //
            // 退栈
            var Context = _LastStack.Pop();

            //
            // 压栈
            _ContextStack.Push(Context);

            //
            //
            if (_ContextStack.Count > 0)
            {
                //
                //
                Context = _ContextStack.Peek();

                if (Context.Content is IStepViewModelContext nextVMContext)
                {
                    nextVMContext.PostContext(Context.Context);
                }
                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
            else
            {
                //
                // 迷之代码部分，这部分是不存在的，如果执行了直接就会错误。
                //
                // Fire Event
                //RaiseEvent(new RoutedEventArgs
                //{
                //    RoutedEvent = DialogCloseEvent
                //});


                ////
                ////
                //ClearValue(DialogPropertyKey);


                ////
                ////
                //_LastStack.Clear();


                ////
                //// 完成提前对话框
                //Context.Session.IsCompleted = false;

                ////
                ////
                //Context.TaskCompletionSource.SetResult(Context.Session);
                throw new InvalidProgramException("出现了意外的情况");
            }
        }

        protected void DoDialogCompletion(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ContextStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            var Context = _ContextStack.Pop();

            //
            // 完成提前对话框
            if (Context.Content is IResultable resultable)
            {
                Context.Session.IsCompleted = resultable.VerifyModel();
            }

            //
            //
            Context.TaskCompletionSource.SetResult(Context.Session);

            //
            //
            if (_ContextStack.Count > 0)
            {
                Context = _ContextStack.Pop();

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
            else
            {

                //
                // Fire Event
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogCloseEvent
                });


                //
                //
                ClearValue(DialogPropertyKey);

                //
                //
                _LastStack.Clear();
            }
        }

        protected void DoDialogSkip(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ContextStack.Count == 0)
            {
                //
                // 错误前返回。
                return;
            }

            var Context = _ContextStack.Pop();

            //
            //
            if (_ContextStack.Count > 0)
            {
                Context = _ContextStack.Pop();

                if (Context.Content is IStepViewModelContext nextVMContext)
                {
                    nextVMContext.PostContext(Context.Context);
                }

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
            else
            {
                //
                // 完成提前对话框
                if (Context.Content is IResultable resultable)
                {
                    Context.Session.IsCompleted = resultable.VerifyModel();
                }

                //
                //
                Context.TaskCompletionSource.SetResult(Context.Session);

                //
                // Fire Event
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogCloseEvent
                });


                //
                //
                ClearValue(DialogPropertyKey);

                //
                //
                _LastStack.Clear();
            }
        }

        protected void DoWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        protected void DoWindowMinimum(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected void DoWindowRestore(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
        protected void DoGoto(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is Type vmType)
            {
                ShellMixins.View(vmType);
            }
        }
        protected void DoGoback(object sender, ExecutedRoutedEventArgs e)
        {
            ShellMixins.Router.NavigateBack.Execute();
        }
        protected void CanDialogNextStep(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_ContextStack.Count > 0)
            {
                e.CanExecute = true;

                if (_ContextStack.Peek().Content is IResultable resultable)
                {
                    e.CanExecute = resultable.VerifyModel();
                }
            }
            else
            {
                e.CanExecute = false;
            }

            

        }
        protected void CanDialogLastStep(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_LastStack.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }

        }

        protected void CanDialogCompletion(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_ContextStack.Count > 0)
            {
                e.CanExecute = true;

                if (_ContextStack.Peek().Content is IResultable resultable)
                {
                    e.CanExecute = resultable.VerifyModel();
                }
            }
            else
            {
                e.CanExecute = false;
            }

        }

        protected void CanDialogSkip(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_ContextStack.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        protected void CanDialogOk(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_ContextStack.Count > 0)
            {
                e.CanExecute = true;

                if (_ContextStack.Peek().Content is IResultable resultable)
                {
                    e.CanExecute = resultable.VerifyModel();
                }
            }
            else
            {
                e.CanExecute = false;
            }

        }
        protected void CanDialogCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            //
            e.CanExecute = _ContextStack.Count > 0;
        }

        protected void CanWindowRestore(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void CanWindowClose(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void CanWindowMinimum(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void CanGoto(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null;
        }
        protected void CanGoback(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((ICommand)ShellMixins.Router.NavigateBack).CanExecute(e.Parameter);
        }

        protected static IRoutableViewModel Get<TViewModel>() where TViewModel : IRoutableViewModel
        {
            return Locator.Current.GetService<TViewModel>();
        }

        protected static IRoutableViewModel Get(Type type)
        {
            if (Locator.CurrentMutable.HasRegistration(type))
            {
                return (IRoutableViewModel)Locator.Current.GetService(type);
            }
            else
            {
                return null;
            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------

        Task<IDialogSession> IDialogManager.Dialog<TViewModel>()
        {
            if (Locator.Current.GetService<TViewModel>() is TViewModel DialogContent)
            {
                var Context = new DialogDisplayContext(new TaskCompletionSource<IDialogSession>(), DialogContent, this);

                //
                // Push DialogContext Stack
                _ContextStack.Push(Context);

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, DialogContent);

                return Context.Task;
            }

            return null;
        }

        Task<IDialogSession> IDialogManager.Dialog(IRoutableViewModel DialogContent)
        {
            if (DialogContent != null)
            {
                var Context = new DialogDisplayContext(new TaskCompletionSource<IDialogSession>(), DialogContent, this);

                //
                // Push DialogContext Stack
                _ContextStack.Push(Context);

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, DialogContent);

                return Context.Task;
            }

            return null;
        }

        Task<IDialogSession> StepCore(IEnumerable<IRoutableViewModel> steps, object context)
        {
            var TaskCompletionSource = new TaskCompletionSource<IDialogSession>();
            var id = Guid.NewGuid();

            foreach (var vm in steps.Reverse())
            {
                if (vm == null)
                {
                    continue;
                }

                _ContextStack.Push(new DialogDisplayContext(TaskCompletionSource, vm, context, id));
            }

            var Context = _ContextStack.Peek();

            if(Context.Content is IStepViewModelContext vmContext)
            {
                vmContext.PostContext(Context.Context);
            }

            //
            // Set New Dialog Content To Property Dialog
            SetValue(DialogPropertyKey, Context.Content);

            return Context.Task;
        }

        Task<IDialogSession> IDialogManager.Step(IEnumerable<Type> steps,object context)
        {
            var array = new IRoutableViewModel[steps.Count()];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Get(steps.ElementAt(i));
            }

            return StepCore(array, context);
        }

        Task<IDialogSession> IDialogManager.Step(IEnumerable<IRoutableViewModel> steps, object context)
        {
            return StepCore(steps, context);
        }

        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
            }, context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
            }, context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3, TStep4>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
                Get<TStep4>(),
            }, context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
                Get<TStep4>(),
                Get<TStep5>(),
            }, context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
                Get<TStep4>(),
                Get<TStep5>(),
                Get<TStep6>(),
            }, context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
                Get<TStep4>(),
                Get<TStep5>(),
                Get<TStep6>(),
                Get<TStep7>()
            },context);
        }
        Task<IDialogSession> IDialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(object context)
        {
            return StepCore(new IRoutableViewModel[]
            {
                Get<TStep1>(),
                Get<TStep2>(),
                Get<TStep3>(),
                Get<TStep4>(),
                Get<TStep5>(),
                Get<TStep6>(),
                Get<TStep7>(),
                Get<TStep8>()
            },context);
        }

        Task<bool> IDialogManager.MessageBox(string title, string content)
        {
            return null;
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Dependency Properties
        //
        //-------------------------------------------------------------------------------------------------
        public object TitleBar
        {
            get => (object)GetValue(TitleBarProperty);
            set => SetValue(TitleBarProperty, value);
        }

        public DataTemplate TitleBarTemplate
        {
            get => (DataTemplate)GetValue(TitleBarTemplateProperty);
            set => SetValue(TitleBarTemplateProperty, value);
        }

        public DataTemplateSelector TitleBarTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TitleBarTemplateSelectorProperty);
            set => SetValue(TitleBarTemplateSelectorProperty, value);
        }

        public string TitleBarStringFormat
        {
            get => (string)GetValue(TitleBarStringFormatProperty);
            set => SetValue(TitleBarStringFormatProperty, value);
        }

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Gets the binding root view model.
        /// </summary>
        public IRoutableViewModel? BindingRoot => ViewModel;

        /// <inheritdoc/>
        public IRoutableViewModel? ViewModel
        {
            get => (IRoutableViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        /// <inheritdoc/>
        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IRoutableViewModel?)value;
        }


        public object Dialog
        {
            get => (object)GetValue(DialogProperty);
            private set => SetValue(DialogPropertyKey, value);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Dependency Properties
        //
        //-------------------------------------------------------------------------------------------------


        /// <summary>
        /// The view model dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(IRoutableViewModel),
                typeof(ShellWindow),
                new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DialogProperty;

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyPropertyKey DialogPropertyKey;

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Brush),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleBarStringFormatProperty = DependencyProperty.Register(
            "TitleBarStringFormat",
            typeof(string),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleBarTemplateSelectorProperty = DependencyProperty.Register(
            "TitleBarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleBarTemplateProperty = DependencyProperty.Register(
            "TitleBarTemplate",
            typeof(DataTemplate),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
            "TitleBar",
            typeof(object),
            typeof(ShellWindow),
            new PropertyMetadata(null));


        //-------------------------------------------------------------------------------------------------
        //
        //  PropertyChangedCallback
        //
        //-------------------------------------------------------------------------------------------------

        private static void OnDialogChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShellWindow window && e.NewValue != null)
            {
                //
                // Fire DialogShow Event
                window.RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogShowEvent
                });
            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  RoutedEvents
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent DialogShowEvent =
            EventManager.RegisterRoutedEvent("DialogShow", RoutingStrategy.Bubble,typeof(EventHandler),typeof(ShellWindow));

        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent DialogCloseEvent =
            EventManager.RegisterRoutedEvent("DialogClose", RoutingStrategy.Bubble,typeof(EventHandler),typeof(ShellWindow));

        //-------------------------------------------------------------------------------------------------
        //
        //  Events
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler DialogClose
        {
            add => AddHandler(DialogCloseEvent, value);
            remove => RemoveHandler(DialogCloseEvent, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler DialogShow
        {
            add => AddHandler(DialogShowEvent, value);
            remove => RemoveHandler(DialogShowEvent, value);
        }
    }
}
