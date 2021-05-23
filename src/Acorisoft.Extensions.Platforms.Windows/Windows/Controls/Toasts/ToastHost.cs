using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Extensions.Windows.Threadings;
using ReactiveUI;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Toasts
{
    public class ToastHost : ContentControl, IToastHost, IToastHostCore
    {
        private static readonly DispatcherTimer ToastDispatcher;
        public static readonly DependencyProperty ToastProperty = DependencyProperty.Register("Toast", typeof(object), typeof(ToastHost), new PropertyMetadata(default(object)));
        
        static ToastHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastHost),new FrameworkPropertyMetadata(typeof(ToastHost)));
            ToastDispatcher = DispatcherTimerFactory.Create(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher);
            ToastDispatcher.Interval = TimeSpan.FromMilliseconds(100);
            ToastDispatcher.Start();
        }

        private readonly Queue<IToastViewModel> _queue;
        private readonly object _sync;
        private IToastViewModel _current;
        public ToastHost()
        {
            _queue = new Queue<IToastViewModel>();
            _sync = new object();
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }

        IDisposable IToastHostCore.SubscribeToastPushing(IObservable<IToastViewModel> observable)
        {
            return observable?.ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(Pushing);
        }

        private void Pushing(IToastViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            lock (_sync)
            {
                _queue.Enqueue(viewModel);
            }
        }

        private void Dispatching(object? sender, EventArgs e)
        {
            if (_queue.Count <= 0)
            {
                if (_current != null && _current.LastAccessBy + _current.Offset < DateTime.Now)
                {
                    Toast = null;
                }
                return;
            }
            
            if (_current is null)
            {
                lock (_sync)
                {
                    _current = _queue.Dequeue();
                    _current.LastAccessBy = DateTime.Now;
                    Toast = new Toast {DataContext = _current};;
                }
            }

            if (_current == null || _current.LastAccessBy + _current.Offset >= DateTime.Now)
            {
                return;
            }
            
            //
            // 当当前内容已经失效的时候
                
            _current = _queue.Dequeue();
            _current.LastAccessBy = DateTime.Now;
            Toast = new Toast {DataContext = _current};;
        }
        
        

        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            ToastDispatcher.Tick -= Dispatching;
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            if (ServiceProvider.GetService(typeof(IViewService)) is IViewService viewService)
            {
                viewService.SetToast(this);
            }
            
            ToastDispatcher.Tick += Dispatching;
        }

        public object Toast
        {
            get => GetValue(ToastProperty);
            set => SetValue(ToastProperty, value);
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