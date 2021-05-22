using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Acorisoft.Extensions.Platform;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Windows;
using Acorisoft.Extensions.Windows.Threadings;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.BusyIndicator
{
    public class BusyIndicator : ContentControl,IBusyIndicator, IBusyIndicatorCore
    {
        private static readonly DispatcherTimer ProgressRunner;
        
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator),new FrameworkPropertyMetadata(typeof(BusyIndicator)));
            ProgressRunner = DispatcherTimerFactory.Create(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher);
            ProgressRunner.Interval = TimeSpan.FromMilliseconds(10);
        }
        

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

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(Xaml.Box(false),OnIsBusyChanged));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(BusyIndicator), new PropertyMetadata(string.Empty));
        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyIndicator));
        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyIndicator));

        // ReSharper disable once InconsistentNaming
        private ProgressBar PART_Progress;
        
        public BusyIndicator()
        {
            this.Loaded += OnLoadedCore;
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
            if (ServiceProvider.GetService(typeof(IViewService)) is IViewService viewService)
            {
                //
                // 设置默认的实现。
                viewService.SetBusyIndicator(this);
            }
            
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
            set => SetValue(IsBusyProperty, Xaml.Box(value)); 
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

        IDisposable IBusyIndicatorCore.SubscribeBusyStateChanged(IObservable<string> observable)
        {
            return observable.ObserveOn(CurrentThreadScheduler.Instance)
                             .SubscribeOn(Xaml.MainThreadScheduler)
                             .Subscribe(x =>
                             {
                                 Description = string.IsNullOrEmpty(x) ? SR.BusyIndicator_DefaultDescription : x;
                             });
        }

        IDisposable IBusyIndicatorCore.SubscribeBusyStateBegin(IObservable<Unit> observable)
        {
            return observable.ObserveOn(CurrentThreadScheduler.Instance)
                .SubscribeOn(Xaml.MainThreadScheduler)
                .Subscribe(x =>
                {
                    IsBusy = true;
                });
        }

        IDisposable IBusyIndicatorCore.SubscribeBusyStateEnd(IObservable<Unit> observable) {
            return observable.ObserveOn(CurrentThreadScheduler.Instance)
                .SubscribeOn(Xaml.MainThreadScheduler)
                .Subscribe(x =>
                {
                    IsBusy = false;
                });
        }
    }
}