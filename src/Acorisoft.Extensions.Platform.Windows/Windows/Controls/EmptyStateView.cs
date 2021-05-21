using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Windows.Controls
{
    public class EmptyStateView : ContentControl, IEmptyStateView
    {
        static EmptyStateView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmptyStateView),new FrameworkPropertyMetadata(typeof(EmptyStateView)));
        }
        
        private static void OnHasContentStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (EmptyStateView) d;
            if ((bool) e.NewValue)
            {
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = ContentOpeningEvent });
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = EmptyStateClosingEvent });
            }
            else
            {
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = ContentClosingEvent });
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = EmptyStateOpeningEvent });
            }
        }
        private static void OnIsEmptyStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (EmptyStateView) d;
            if ((bool) e.NewValue)
            {
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = ContentClosingEvent });
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = EmptyStateOpeningEvent });
            }
            else
            {
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = ContentOpeningEvent });
                element.RaiseEvent(new RoutedEventArgs{ RoutedEvent = EmptyStateClosingEvent });
            }
        }
        
        public static readonly DependencyProperty HasContentStateProperty = DependencyProperty.Register("HasContentState",
            typeof(bool), typeof(EmptyStateView), new PropertyMetadata(BooleanBoxes.Box(false), OnHasContentStateChanged));

        public static readonly DependencyProperty IsEmptyStateProperty = DependencyProperty.Register("IsEmptyState",
            typeof(bool), typeof(EmptyStateView), new PropertyMetadata(BooleanBoxes.Box(false), OnIsEmptyStateChanged));
        public static readonly DependencyProperty EmptyStateStringFormatProperty = DependencyProperty.Register(
            "EmptyStateStringFormat",
            typeof(string),
            typeof(EmptyStateView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyStateTemplateSelectorProperty = DependencyProperty.Register(
            "EmptyStateTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(EmptyStateView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyStateTemplateProperty = DependencyProperty.Register(
            "EmptyStateTemplate",
            typeof(DataTemplate),
            typeof(EmptyStateView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyStateProperty = DependencyProperty.Register(
            "EmptyState",
            typeof(object),
            typeof(EmptyStateView),
            new PropertyMetadata(null));

        public static readonly RoutedEvent EmptyStateOpeningEvent = EventManager.RegisterRoutedEvent("EmptyStateOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EmptyStateView));
        public static readonly RoutedEvent EmptyStateClosingEvent = EventManager.RegisterRoutedEvent("EmptyStateClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EmptyStateView));
        public static readonly RoutedEvent ContentOpeningEvent = EventManager.RegisterRoutedEvent("ContentOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EmptyStateView));
        public static readonly RoutedEvent ContentClosingEvent = EventManager.RegisterRoutedEvent("ContentClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EmptyStateView));
        
        public object EmptyState
        {
            get => (object)GetValue(EmptyStateProperty);
            set => SetValue(EmptyStateProperty, value);
        }

        public DataTemplate EmptyStateTemplate
        {
            get => (DataTemplate)GetValue(EmptyStateTemplateProperty);
            set => SetValue(EmptyStateTemplateProperty, value);
        }

        public DataTemplateSelector EmptyStateTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(EmptyStateTemplateSelectorProperty);
            set => SetValue(EmptyStateTemplateSelectorProperty, value);
        }

        public string EmptyStateStringFormat
        {
            get => (string)GetValue(EmptyStateStringFormatProperty);
            set => SetValue(EmptyStateStringFormatProperty, value);
        }
        
        public bool HasContentState
        {
            get => (bool) GetValue(HasContentStateProperty);
            set => SetValue(HasContentStateProperty, BooleanBoxes.Box(value));
        }
        public bool IsEmptyState
        {
            get => (bool) GetValue(IsEmptyStateProperty);
            set => SetValue(IsEmptyStateProperty, BooleanBoxes.Box(value));
        }
        
        public event RoutedEventHandler EmptyStateOpening
        {
            add => AddHandler(EmptyStateOpeningEvent, value);
            remove => RemoveHandler(EmptyStateOpeningEvent, value);
        }
        public event RoutedEventHandler EmptyStateClosing
        {
            add => AddHandler(EmptyStateClosingEvent, value);
            remove => RemoveHandler(EmptyStateClosingEvent, value);
        }
        public event RoutedEventHandler ContentOpening
        {
            add => AddHandler(ContentOpeningEvent, value);
            remove => RemoveHandler(ContentOpeningEvent, value);
        }
        
        public event RoutedEventHandler ContentClosing
        {
            add => AddHandler(ContentClosingEvent, value);
            remove => RemoveHandler(ContentClosingEvent, value);
        }
    }
}