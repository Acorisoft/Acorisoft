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
using System.Windows.Threading;
using Acorisoft.Extensions.Platforms.Windows.Commands;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.Threadings;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public partial class SpaWindow : Window
    {
        static SpaWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpaWindow),new FrameworkPropertyMetadata(typeof(SpaWindow)));
            //CommandRefresher = DispatcherTimerFactory.Create(DispatcherPriority.Normal);
            //CommandRefresher.Tick += (_, _) =>
            //{
            //    CommandManager.InvalidateRequerySuggested();

            //};
            //CommandRefresher.Interval = TimeSpan.FromMilliseconds(300);
            //CommandRefresher.Start();

        }

        private static readonly DispatcherTimer CommandRefresher;

        protected SpaWindow() : base()
        {
            
            CommandBindings.Add(new CommandBinding(WindowCommands.Cancel, OnDialogCancel, CanDialogCancel));
            CommandBindings.Add(new CommandBinding(WindowCommands.Completed, OnDialogNextOrComplete, CanDialogNextOrComplete));
            CommandBindings.Add(new CommandBinding(WindowCommands.Ignore, OnDialogIgnoreOrSkip, CanDialogIgnoreOrSkip));
            CommandBindings.Add(new CommandBinding(WindowCommands.Last, OnDialogLast, CanDialogLast));
            CommandBindings.Add(new CommandBinding(WindowCommands.Next, OnDialogNextOrComplete, CanDialogNextOrComplete));
            CommandBindings.Add(new CommandBinding(WindowCommands.Skip, OnDialogIgnoreOrSkip, CanDialogIgnoreOrSkip));

            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnWindowClose));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnWindowMinimum));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnWindowRestore));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnWindowRestore));
            CommandBindings.Add(new CommandBinding(IxContentHostCommands.ToggleEnable, ToggleEnable));
        }


        #region WindowCommands


        private void OnDialogCancel(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.Cancel();
        }

        private void OnDialogNextOrComplete(object sender, ExecutedRoutedEventArgs e)
        {

            ServiceLocator.DialogService.NextOrComplete();
        }

        private void OnDialogIgnoreOrSkip(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.IgnoreOrSkip();
        }


        private void OnDialogLast(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.Last();
        }


        private void CanDialogCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanCancel();
            e.Handled = true;
        }

        private void CanDialogNextOrComplete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanNextOrComplete();
            e.Handled = true;
        }

        private void CanDialogIgnoreOrSkip(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanIgnoreOrSkip();
            e.Handled = true;
        }

        private void CanDialogLast(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanLast();
            e.Handled = true;
        }
        #endregion

        #region DependencyProperties

        // /// <summary>
        // /// The view model dependency property.
        // /// </summary>
        // public static readonly DependencyProperty ViewModelProperty =
        //     DependencyProperty.Register(
        //         "ViewModel",
        //         typeof(IRoutableViewModel),
        //         typeof(SpaWindow),
        //         new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarStringFormatProperty = DependencyProperty.Register(
            "TitleBarStringFormat",
            typeof(string),
            typeof(SpaWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarTemplateSelectorProperty = DependencyProperty.Register(
            "TitleBarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(SpaWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarTemplateProperty = DependencyProperty.Register(
            "TitleBarTemplate",
            typeof(DataTemplate),
            typeof(SpaWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
            "TitleBar",
            typeof(object),
            typeof(SpaWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Brush),
            typeof(SpaWindow),
            new PropertyMetadata(null));

        #endregion DependencyProperties

        #region SystemCommands


        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowMinimum(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnWindowRestore(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }


        #endregion SystemCommands

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

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


        void ToggleEnable(object sender, ExecutedRoutedEventArgs e)
        {
            SwipeRecognitor.IsEnable = !SwipeRecognitor.IsEnable;
        }
    }
}
