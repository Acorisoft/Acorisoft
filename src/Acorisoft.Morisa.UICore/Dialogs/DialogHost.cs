using Acorisoft.Morisa.Logs;
using ReactiveUI;
using Splat;
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

namespace Acorisoft.Morisa.Dialogs
{
    /// <summary>    
    ///
    /// </summary>
    public class DialogHost : Control
    {
        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));
            ViewModelPropertyKey = DependencyProperty.RegisterReadOnly(
                "ViewModel",
                typeof(object),
                typeof(DialogHost),
                null);
            ViewModelProperty = ViewModelPropertyKey.DependencyProperty;
        }

        private struct DialogResultContext
        {
            internal TaskCompletionSource<DialogResult> tcs;
            internal DialogResult result;
            internal IRoutableViewModel vm;
        }

        private readonly Stack<DialogResultContext> _stack;
        private IFullLogger _logger;

        public DialogHost()
        {
            _stack = new Stack<DialogResultContext>();
            CommandBindings.Add(new CommandBinding(DialogCommands.Ok, OnDialogOk, OnCanDialogOk));
            CommandBindings.Add(new CommandBinding(DialogCommands.Cancel, OnDialogCancel, OnCanDialogCancel));
        }

        private void OnCanDialogOk(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _stack.Count > 0;
        }
        private void OnCanDialogCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _stack.Count > 0;
        }

        private void OnDialogOk(object sender, ExecutedRoutedEventArgs e)
        {
            if (_stack.Count == 0)
            {
                //
                // 无效确定指令
                _logger.Error("对话框堆栈中没有正在打开的对话框,无法确认");

                //
                // 错误前返回。
                return;
            }

            var context = _stack.Pop();

            //
            // 设置完成属性
            context.result.IsCompleted = true;

            //
            // 完成任务。
            context.tcs.SetResult(context.result);

            //
            // fixed: issues #1
            // 修复了对话框结果为空的错误。
            context.result.Result = context.vm;

            //
            //
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = DialogCloseEvent
            });

#if DEBUG
            _logger.Info("对话框返回，结果错误");
#endif
        }

        private void OnDialogCancel(object sender, ExecutedRoutedEventArgs e)
        {
            if(_stack.Count == 0)
            {
                //
                // 无效确定指令
                _logger.Error("对话框堆栈中没有正在打开的对话框，无法取消");

                //
                // 错误前返回。
                return;
            }

            var context = _stack.Pop();

            //
            // 设置完成属性
            context.result.IsCompleted = false;

            //
            // 完成任务。
            context.tcs.SetResult(context.result);
            //
            //
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = DialogCloseEvent
            });

#if DEBUG
            _logger.Info("对话框返回，结果正确");
#endif
        }

        public DialogManager Manager {
            get => (DialogManager)GetValue(ManagerProperty);
            set => SetValue(ManagerProperty, value);
        }
        public object ViewModel {
            get => (object)GetValue(DialogServiceProperty);
        }

        public static readonly DependencyPropertyKey DialogServicePropertyKey;
        public static readonly DependencyProperty DialogServiceProperty;
        public static readonly DependencyPropertyKey ViewModelPropertyKey;
        public static readonly DependencyProperty ViewModelProperty;
        public static readonly DependencyProperty ManagerProperty = DependencyProperty.Register(
            "Manager",
            typeof(DialogManager),
            typeof(DialogHost),
            new PropertyMetadata(OnManagerChanged));


        public static readonly RoutedEvent DialogShowEvent =
            EventManager.RegisterRoutedEvent("DialogShow", RoutingStrategy.Bubble,typeof(EventHandler),typeof(DialogHost));

        public static readonly RoutedEvent DialogCloseEvent =
            EventManager.RegisterRoutedEvent("DialogClose", RoutingStrategy.Bubble,typeof(EventHandler),typeof(DialogHost));

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

        private static void OnManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is DialogManager dialogMgr && d is DialogHost dialogHost)
            {
                //
                // 监听
                dialogMgr.DemandDialogShow += dialogHost.OnDialogShow;
                dialogHost._logger = dialogMgr.GetLogger();

                //
                // 取消监听
                if (e.OldValue is DialogManager oldMgr)
                {
                    oldMgr.DemandDialogShow -= dialogHost.OnDialogShow;
                }
            }
        }

        private void OnDialogShow(object sender, DialogShowEventArgs e)
        {
            var context = new DialogResultContext
            {
                tcs = e.TCS,
                result = e.Result,
                vm = e.ViewModel
            };

            //
            // 将上下文放入对话框对战中。
            _stack.Push(context);

            //
            //
            SetValue(ViewModelPropertyKey, e.ViewModel);

            //
            //
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = DialogShowEvent
            });
        }
    }
}
