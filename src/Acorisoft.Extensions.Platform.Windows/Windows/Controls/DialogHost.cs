using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Windows.Controls
{
    public class DialogHost : ContentControl, IDialogHost
    {
        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost),new FrameworkPropertyMetadata(typeof(DialogHost)));
        }

        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DialogHost));
        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DialogHost));
        public static readonly DependencyProperty DialogProperty = DependencyProperty.Register("Dialog", typeof(object), typeof(DialogHost), new PropertyMetadata(default(object), OnDialogChanged));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(DialogHost), new PropertyMetadata(BooleanBoxes.False, OnIsOpenChanged));
        
        private static void OnDialogChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = (DialogHost) d;
            
            if (e.NewValue is null && e.OldValue is not null)
            {
                //
                // force dialog Closing
                host.IsOpen = false;
            }

            if (e.OldValue is null && e.NewValue is not null)
            {
                //
                // force dialog Opening
                host.IsOpen = true;
            }

            if (e.OldValue is not null && e.NewValue is not null)
            {
                //
                // force dialog Opening
                host.IsOpen = true;
            }
        }
        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = (DialogHost) d;

            if ((bool) e.NewValue)
            {
                host.RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent =  DialogOpeningEvent
                });
            }
            else
            {
                host.RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent =  DialogClosingEvent
                }); 
            }
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, BooleanBoxes.Box(value));
        }

        public event RoutedEventHandler DialogOpening
        {
            add => AddHandler(DialogOpeningEvent, value);
            remove => RemoveHandler(DialogOpeningEvent, value);
        }
        
        public event RoutedEventHandler DialogClosing
        {
            add => AddHandler(DialogClosingEvent, value);
            remove => RemoveHandler(DialogClosingEvent, value);
        }
        
        public object Dialog
        {
            get => GetValue(DialogProperty);
            set => SetValue(DialogProperty, value);
        }
    }
}