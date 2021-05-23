using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class ToastHost : ContentControl
    {
        public ToastHost()
        {
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
        }
    }

    public class Toast : ContentControl
    {
        static Toast()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toast),new FrameworkPropertyMetadata(typeof(Toast)));
        }
        public Toast()
        {
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }
        
        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = ClosingEvent
            });
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = OpeningEvent
            });
        }
        
        public static readonly RoutedEvent OpeningEvent = EventManager.RegisterRoutedEvent("Opening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));
        public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent("Closing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));
        public event RoutedEventHandler Opening
        {
            add => AddHandler(OpeningEvent, value);
            remove => RemoveHandler(OpeningEvent, value);
        }
        
        public event RoutedEventHandler Closing
        {
            add => AddHandler(ClosingEvent, value);
            remove => RemoveHandler(ClosingEvent, value);
        }
    }
}