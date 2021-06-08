using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Threadings;
using ReactiveUI;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.ActivityIndicator
{
    public class ActivityIndicator : ContentControl, IActivityIndicator, IActivityIndicatorCore
    {
        static ActivityIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivityIndicator),new FrameworkPropertyMetadata(typeof(ActivityIndicator)));
        }
        

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = (ActivityIndicator) d;
            var result = (bool) e.NewValue;
            if (result)
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

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(ActivityIndicator), new PropertyMetadata(Xaml.Box(false),OnIsBusyChanged));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(ActivityIndicator), new PropertyMetadata(string.Empty));
        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));
        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));
        
        public ActivityIndicator()
        {
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedCore;
        }
        
        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
        }

        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
            if (ServiceProvider.GetService(typeof(IViewService)) is IViewService viewService)
            {
                //
                // 设置默认的实现。
                viewService.SetActivityIndicator(this);
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

        IDisposable IActivityIndicatorCore.SubscribeActivityChanged(IObservable<string> observable)
        {
            return observable?.ObserveOn(RxApp.MainThreadScheduler)
                             .Subscribe(x =>
                             {
                                 Description = string.IsNullOrEmpty(x) ? SR.ActivityIndicator_DefaultDescription : x;
                             });
        }

        IDisposable IActivityIndicatorCore.SubscribeActivityBegin(IObservable<Unit> observable)
        {
            return observable?.ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    IsBusy = true;
                });
        }

        IDisposable IActivityIndicatorCore.SubscribeActivityEnd(IObservable<Unit> observable) {
            return observable?.ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    IsBusy = false;
                });
        }
    }
}