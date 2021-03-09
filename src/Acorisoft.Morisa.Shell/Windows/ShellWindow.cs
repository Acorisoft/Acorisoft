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

        protected class DialogSession : IDialogSession
        {
            //-------------------------------------------------------------------------------------------------
            //
            //  Constructors
            //
            //-------------------------------------------------------------------------------------------------
            public DialogSession(bool result, IRoutableViewModel viewModel,IDialogManager manager)
            {
                IsCompleted = result;
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
                return ViewModel as T;
            }

            //-------------------------------------------------------------------------------------------------
            //
            //  Properties
            //
            //-------------------------------------------------------------------------------------------------

            protected internal IDialogManager Manager { get; }

            public bool IsCompleted { get; }
            public IRoutableViewModel ViewModel { get; }
        }

        protected class DialogDisplayContext
        {
            //-------------------------------------------------------------------------------------------------
            //
            //  Constructors
            //
            //-------------------------------------------------------------------------------------------------
            public DialogDisplayContext(TaskCompletionSource<IDialogSession> tcs, IRoutableViewModel content)
            {
                TaskCompletionSource = tcs;
                Task = tcs.Task;
                Content = content;
            }
            //-------------------------------------------------------------------------------------------------
            //
            //  Properties
            //
            //-------------------------------------------------------------------------------------------------
            public TaskCompletionSource<IDialogSession> TaskCompletionSource { get; }
            public Task<IDialogSession> Task { get; }
            public IRoutableViewModel Content { get; }

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

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public ShellWindow()
        {
            _ContextStack = new Stack<DialogDisplayContext>();

            CommandBindings.Add(new CommandBinding(DialogCommands.Ok, DoDialogOk, CanDialogOk));
            CommandBindings.Add(new CommandBinding(DialogCommands.Cancel, DoDialogCancel, CanDialogCancel));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, DoWindowClose, CanWindowClose));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, DoWindowMinimum, CanWindowMinimum));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, DoWindowRestore, CanWindowRestore));

            Locator.CurrentMutable.RegisterConstant<IDialogManager>(this);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  CommandBindings
        //
        //-------------------------------------------------------------------------------------------------

        protected void DoDialogOk(object sender, RoutedEventArgs e)
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
            Context.TaskCompletionSource.SetResult(new DialogSession(true, Context.Content, this));

            //
            // Fire Event
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = DialogCloseEvent
            });

            //
            //
            if(_ContextStack.Count > 0)
            {
                Context = _ContextStack.Pop();

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
        }

        protected void DoDialogCancel(object sender, RoutedEventArgs e)
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
            Context.TaskCompletionSource.SetResult(new DialogSession(false, Context.Content, this));

            //
            // Fire Event
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = DialogCloseEvent
            });

            //
            //
            if (_ContextStack.Count > 0)
            {
                Context = _ContextStack.Pop();

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, Context.Content);
            }
        }

        protected void DoWindowClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void DoWindowMinimum(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected void DoWindowRestore(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        protected void CanDialogOk(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _ContextStack.Count > 0;
        }
        protected void CanDialogCancel(object sender, CanExecuteRoutedEventArgs e)
        {
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
        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------

        Task<IDialogSession> IDialogManager.Dialog<TViewModel>()
        {
            if (Locator.Current.GetService<TViewModel>() is TViewModel DialogContent)
            {
                //
                // Push DialogContext Stack
                _ContextStack.Push(new DialogDisplayContext(new TaskCompletionSource<IDialogSession>(), DialogContent));

                //
                // Set New Dialog Content To Property Dialog
                SetValue(DialogPropertyKey, DialogContent);

            }

            return null;
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
        public static readonly DependencyProperty DialogProperty;
        public static readonly DependencyPropertyKey DialogPropertyKey;


        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Brush),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarStringFormatProperty = DependencyProperty.Register(
            "TitleBarStringFormat",
            typeof(string),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarTemplateSelectorProperty = DependencyProperty.Register(
            "TitleBarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(ShellWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarTemplateProperty = DependencyProperty.Register(
            "TitleBarTemplate",
            typeof(DataTemplate),
            typeof(ShellWindow),
            new PropertyMetadata(null));

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
            if (d is ShellWindow window)
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

        public static readonly RoutedEvent DialogShowEvent =
            EventManager.RegisterRoutedEvent("DialogShow", RoutingStrategy.Bubble,typeof(EventHandler),typeof(ShellWindow));

        public static readonly RoutedEvent DialogCloseEvent =
            EventManager.RegisterRoutedEvent("DialogClose", RoutingStrategy.Bubble,typeof(EventHandler),typeof(ShellWindow));

        //-------------------------------------------------------------------------------------------------
        //
        //  Events
        //
        //-------------------------------------------------------------------------------------------------
        public event RoutedEventHandler DialogClose
        {
            add => AddHandler(DialogCloseEvent, value);
            remove => RemoveHandler(DialogCloseEvent, value);
        }

        public event RoutedEventHandler DialogShow
        {
            add => AddHandler(DialogShowEvent, value);
            remove => RemoveHandler(DialogShowEvent, value);
        }
    }
}
