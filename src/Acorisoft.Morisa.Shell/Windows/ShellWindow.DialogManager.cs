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
using Acorisoft.Dialogs;
using Acorisoft.ViewModels;
using static Acorisoft.Dialogs.DialogManager;
using Acorisoft.Properties;

namespace Acorisoft.Windows
{
    partial class ShellWindow
    {
        private readonly DialogManager _DialogManager;
        private DisplayContext _Context;

        public ShellWindow()
        {
            _DialogManager = (DialogManager)Locator.Current.GetService<IDialogManager>();

            Initialize();

            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
            this.Closed += OnClosed;
        }

        protected virtual void OnClosed(object sender, EventArgs e)
        {

            _DialogManager.DialogChanged -= OnDialogChanged;
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Initialize Methods
        //
        //-------------------------------------------------------------------------------------------------
        void Initialize()
        {
            InitializeDialogCommands();

            //
            // 初始化
            InitializeDialogManager();
        }

        void InitializeDialogCommands()
        {
            //
            // 绑定命令
            CommandBindings.Add(new CommandBinding(WindowsCommands.Ok, DoOk, CanOk));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Cancel, DoCancel, CanCancel));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Next, DoNext, CanNext));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Last, DoLast, CanLast));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Ignore, DoIgnore, CanIgnore));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, DoWindowClose, CanWindowClose));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, DoWindowMinimum, CanWindowMinimum));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, DoWindowRestore, CanWindowRestore));
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Commands Methods
        //
        //-------------------------------------------------------------------------------------------------
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

        void CanOk(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            // 堆栈中存在当前对话框的上下文
            e.CanExecute = _DialogManager.Count > 0 && _Context != null;
            e.Handled = true;
        }

        void CanCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            // 堆栈中存在当前对话框的上下文
            e.CanExecute = _DialogManager.Count > 0 && _Context != null;
            e.Handled = true;
        }

        void CanNext(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            // 堆栈中存在当前对话框的上下文
            e.CanExecute = _DialogManager.Count > 0 && _Context is StepDialogContext context && context.CanNext;
            e.Handled = true;
        }

        void CanLast(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            // 堆栈中存在当前对话框的上下文
            e.CanExecute = _DialogManager.Count > 0 && _Context is StepDialogContext context && context.CanNext;
            e.Handled = true;
        }

        void CanIgnore(object sender, CanExecuteRoutedEventArgs e)
        {
            //
            // 堆栈中存在当前对话框的上下文
            e.CanExecute = _DialogManager.Count > 0 && _Context is StepDialogContext context && context.CanIgnore;
            e.Handled = true;
        }

        void DoOk(object sender, ExecutedRoutedEventArgs e)
        {
            if(_Context is DialogDisplayContext ddc && ddc.Session is not null)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Ok);

                //
                // 设置 Session 的结果
                ddc.Session.SetResult(ddc.ViewModel);

                //
                // 设置 Session标志
                ddc.Session.IsCompleted = true;

                //
                // 设置当前TCS的结果
                ddc.TaskCompletionSource.SetResult(ddc.Session);

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }
            else if(_Context is NotificationDisplayContext ndc)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Ok);

                //
                // 设置当前TCS的结果
                ndc.TaskCompletionSource.SetResult();

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }

            else if (_Context is ConfirmDisplayContext cdc)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Ok);

                //
                // 设置当前TCS的结果
                cdc.TaskCompletionSource.SetResult(true);

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }

            if (_DialogManager.Count > 0)
            {
                _DialogManager.Resume();
            }
            else
            {
                //
                // 如果没有上下文则退出。
                RaiseEvent(new RoutedEventArgs { RoutedEvent = DialogCloseEvent });
            }
        }

        void DoCancel(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Context is DialogDisplayContext ddc && ddc.Session is not null)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Ok);

                //
                // 设置 Session标志
                ddc.Session.IsCompleted = false;

                //
                // 设置当前TCS的结果
                ddc.TaskCompletionSource.SetResult(null);

                //
                // 设置当前TCS的结果
                ddc.TaskCompletionSource.SetResult(ddc.Session);

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }
            else if (_Context is NotificationDisplayContext ndc)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Cancel);

                //
                // 设置当前TCS的结果
                ndc.TaskCompletionSource.SetResult();

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }

            else if (_Context is ConfirmDisplayContext cdc)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Cancel);

                //
                // 设置当前TCS的结果
                cdc.TaskCompletionSource.SetResult(false);

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }
            else if(_Context is StepDialogContext sdc)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Dialog_Cancel);

                //
                // 设置 Session标志
                sdc.Session.IsCompleted = false;

                //
                // 设置当前TCS的结果
                sdc.TaskCompletionSource.SetResult(null);

                //
                // 设置当前TCS的结果
                sdc.TaskCompletionSource.SetResult(sdc.Session);

                //
                // 弹出当前上下文
                _DialogManager.Pop();
            }

            if (_DialogManager.Count > 0)
            {
                _DialogManager.Resume();
            }
            else
            {
                //
                // 如果没有上下文则退出。
                RaiseEvent(new RoutedEventArgs { RoutedEvent = DialogCloseEvent });
            }
        }

        void DoNext(object sender, ExecutedRoutedEventArgs e)
        {
            if(_Context is StepDialogContext sdc && sdc.Session is not null)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Step_Next);

                if(sdc.Count == 0)
                {
                    //
                    // 设置 Session 的结果
                    sdc.Session.SetResult(sdc.Context);

                    //
                    // 设置 Session标志
                    sdc.Session.IsCompleted = true;

                    //
                    // 弹出当前上下文
                    _DialogManager.Pop();
                }
                else
                {
                    //
                    // 
                    sdc.Next();

                    //
                    //
                    SetValue(DialogProperty, _Context.ViewModel);
                }

            }


            if (_DialogManager.Count > 0)
            {
                _DialogManager.Resume();
            }
            else
            {
                //
                // 如果没有上下文则退出。
                RaiseEvent(new RoutedEventArgs { RoutedEvent = DialogCloseEvent });
            }
        }

        void DoLast(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Context is StepDialogContext sdc && sdc.Session is not null)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Step_Next);

                if (sdc.Count > 0)
                {
                    //
                    // 
                    sdc.Last();

                    //
                    //
                    SetValue(DialogProperty, _Context.ViewModel);
                }

            }
        }

        void DoIgnore(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Context is StepDialogContext sdc && sdc.Session is not null)
            {
                //
                // 日志记录
                _DialogManager.Logger.Debug(SR.DialogManager_Step_Ignore);

                if (sdc.Count > 0)
                {
                    //
                    // 
                    sdc.Next();

                    //
                    //
                    SetValue(DialogProperty, _Context.ViewModel);
                }

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

        void InitializeDialogManager()
        {
            _DialogManager.DialogChanged += OnDialogChanged;
        }

        private void OnDialogChanged(DisplayContext context)
        {
            if(context == null)
            {
                return;
            }

            _Context = context;

            //
            // 释放视图
            SetValue(DialogPropertyKey, context.ViewModel);
        }
    }
}
