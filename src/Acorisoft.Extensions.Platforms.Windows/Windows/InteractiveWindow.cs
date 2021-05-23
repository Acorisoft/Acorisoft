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
using Acorisoft.Extensions.Platforms.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public partial class SpaWindow : Window
    {
        static SpaWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpaWindow),new FrameworkPropertyMetadata(typeof(SpaWindow)));
            
        }
        protected SpaWindow() : base()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnWindowClose));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnWindowMinimum));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnWindowRestore));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnWindowRestore));
            CommandBindings.Add(new CommandBinding(IxContentHostCommands.ToggleEnable, ToggleEnable));
        }

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
