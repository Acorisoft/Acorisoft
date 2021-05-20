using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using Acorisoft.Extensions.Windows.Commands;
using Acorisoft.Extensions.Windows.Dialogs;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.ViewModels;
using ReactiveUI;
using Splat;

namespace Acorisoft.Extensions.Windows
{
    public abstract class InteractiveWindow : Window
    {
        protected InteractiveWindow()
        {
            _dialogContextStack = new Stack<DialogContext>();
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
            this.DataContextChanged+= OnDataContextChanged;
            DataContextProperty.OverrideMetadata(typeof(InteractiveWindow),new PropertyMetadata(null,OnDataContextChanged));
            CommandBindings.Add(new CommandBinding(WindowCommands.Cancel, OnDialogCancel));
            CommandBindings.Add(new CommandBinding(WindowCommands.Completed, OnDialogNextOrComplete,CanDialogNextOrComplete));
            CommandBindings.Add(new CommandBinding(WindowCommands.Ignore, OnDialogSkipOrIgnore, CanDialogSkipOrIgnore));
            CommandBindings.Add(new CommandBinding(WindowCommands.Last, OnDialogLast,CanDialogLast));
            CommandBindings.Add(new CommandBinding(WindowCommands.Next, OnDialogNextOrComplete,CanDialogNextOrComplete));
            CommandBindings.Add(new CommandBinding(WindowCommands.Skip, OnDialogSkipOrIgnore, CanDialogSkipOrIgnore));
        }

        #region DialogCommands
        
        private void OnDialogClosing(object? sender, EventArgs e)
        {
            if (_dialogContextStack.Count == 0)
            {
                return;
            }
            _dialogContextStack.Pop();
        }

        private void OnWizardShowing(object sender, WizardShowingEventArgs e)
        {
            if (e is null)
            {
                return;
            }
            
            _dialogContextStack.Push(e.Context);
        }

        private void OnDialogShowing(object sender, DialogShowingEventArgs e)
        {
            if (e is null)
            {
                return;
            }
            
            _dialogContextStack.Push(e.Context);
        }

        private void OnPromptShowing(object sender, PromptShowingEventArgs e)
        {
            if (e is null)
            {
                return;
            }
            
            _dialogContextStack.Push(e.Context);
        }


        private readonly Stack<DialogContext> _dialogContextStack;
        private DialogContext CurrentDialogContext => _dialogContextStack.Count == 0 ? null : _dialogContextStack.Peek();

        private void CanDialogLast(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentDialogContext is StackedDialogContext sdc && sdc.CanLast();
            e.Handled = true;
        }

        private void OnDialogLast(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentDialogContext is not StackedDialogContext context)
            {
                return;
            }

            if (!context.CanLast())
            {
                return;
            }
            
            context.Last();
        }
        private void CanDialogNextOrComplete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentDialogContext is not null && CurrentDialogContext.ViewModel.VerifyAccess();
            e.Handled = true;
        }
        private void CanDialogSkipOrIgnore(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (CurrentDialogContext is StackedDialogContext sdc && sdc.ViewModel.CanIgnore()) || true;
            e.Handled = true;
        }
        
        private void OnDialogNextOrComplete(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentDialogContext is null)
            {
                return;
                
            }

            if (!CurrentDialogContext.ViewModel.VerifyAccess())
            {
                return;
            }
            
            CurrentDialogContext.NextOrComplete();
        }
        
        private void OnDialogSkipOrIgnore(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentDialogContext is null)
            {
                return;
                
            }

            if (!CurrentDialogContext.ViewModel.CanIgnore())
            {
                return;
            }
            CurrentDialogContext.Ignore();
        }

        private void OnDialogCancel(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentDialogContext is null)
            {
                return;
                
            }
            
            CurrentDialogContext.Cancel();
        }

        #endregion

        #region DataContextChanged
        private void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IViewModelLifeCycle oldImpl)
            {
                oldImpl.Stop();
            }

            if (e.NewValue is IViewModelLifeCycle newImpl)
            {
                newImpl.Start();
            }
        }
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
        
        #endregion

        #region Loaded & Unloaded


        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoadedCore;
            this.Unloaded -= OnUnloadedCore;
            this.DataContextChanged -= OnDataContextChanged;
            Platform.ViewService.Navigating -= OnNavigating;
            Platform.DialogService.PromptShowing -= OnPromptShowing;
            Platform.DialogService.DialogShowing -= OnDialogShowing;
            Platform.DialogService.WizardShowing -= OnWizardShowing;
            Platform.DialogService.DialogClosing -= OnDialogClosing;
            
            OnUnloaded(sender, e);
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            Platform.ViewService.Navigating += OnNavigating;
            Platform.DialogService.PromptShowing += OnPromptShowing;
            Platform.DialogService.DialogShowing += OnDialogShowing;
            Platform.DialogService.WizardShowing += OnWizardShowing;
            Platform.DialogService.DialogClosing += OnDialogClosing;
            OnLoaded(sender, e);
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion
        
        #region ViewService Impl

        protected virtual void OnNavigating(object sender, NavigateToViewEventArgs e)
        {
            Platform.ScreenService
                    .Router
                    .Navigate
                    .Execute((PageViewModel)e.Current);
        }

        #endregion

    }
}