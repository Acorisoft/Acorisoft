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

namespace Acorisoft.Windows
{
#pragma warning disable CS8632

    public abstract partial class ShellWindow : Window, IViewFor
    {
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
