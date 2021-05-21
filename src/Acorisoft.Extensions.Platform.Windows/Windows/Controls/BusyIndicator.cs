using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Acorisoft.Extensions.Windows.Threadings;

namespace Acorisoft.Extensions.Windows.Controls
{
    public class BusyIndicator : ContentControl, IBusyIndicator
    {
        private static readonly DispatcherTimer ProgressRunner;
        
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator),new FrameworkPropertyMetadata(typeof(BusyIndicator)));
            ProgressRunner = DispatcherTimerFactory.Create(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher);
            ProgressRunner.Interval = TimeSpan.FromMilliseconds(10);
        }
        
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(BooleanBoxes.Box(false),OnIsBusyChanged));

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = (BusyIndicator) d;
            
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

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(BusyIndicator), new PropertyMetadata(string.Empty));
        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyIndicator));
        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyIndicator));

        private ProgressBar PART_Progress;
        
        public BusyIndicator()
        {
            this.Loaded+= OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }

        public override void OnApplyTemplate()
        {
            PART_Progress = (ProgressBar) GetTemplateChild("PART_Progress");
            base.OnApplyTemplate();
        }

        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            ProgressRunner.Tick -= OnTick;
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            ProgressRunner.Tick += OnTick;
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (PART_Progress != null)
            {
                PART_Progress.Value += 1;
            }
        }

        public string Description
        {
            get => (string) GetValue(DescriptionProperty); 
            set => SetValue(DescriptionProperty, value); 
        }
        
        public bool IsBusy
        {
            get => (bool) GetValue(IsBusyProperty); 
            set => SetValue(IsBusyProperty, BooleanBoxes.Box(value)); 
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
    }
}