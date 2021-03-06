using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using Acorisoft.Extensions.Windows.Services;
using Acorisoft.Extensions.Windows.Threadings;
using MediatR;
using ReactiveUI;
using Splat;

namespace Acorisoft.Extensions.Windows.Primitives
{
    public class ToastTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not IToastViewModel toastViewModel)
            {
                return Info;
            }

            return toastViewModel.MessageType switch
            {
                MessageType.Error => Error,
                MessageType.Info => Info,
                MessageType.Success => Success,
                _ => Warning
            };
        }

        public DataTemplate Info { get; set; }
        public DataTemplate Success { get; set; }
        public DataTemplate Warning { get; set; }
        public DataTemplate Error { get; set; }
        public DataTemplate Custom { get; set; }
    }
    
    /// <summary>
    /// <see cref="ToastHost"/> 表示一个信息推送器。
    /// </summary>
    [DefaultProperty("Content")]
    [ContentProperty("Content")]
    public class ToastHost : ContentControl
    {
        //--------------------------------------------------------------------------------------------------------------
        //
        // Static Fields
        //
        //--------------------------------------------------------------------------------------------------------------

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
        private IDisposable _disposable;
        
        public ToastHost()
        {
            _queue = new Queue<IToastViewModel>();
            _sync = new object();
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }

        // IDisposable IToastHostCore.SubscribeToastPushing(IObservable<IToastViewModel> observable)
        // {
        //     return observable?.ObserveOn(RxApp.MainThreadScheduler)
        //         .Subscribe(Pushing);
        // }

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
            Toast = new Toast
            {
                DataContext = _current
            };;
        }
        
        

        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            _disposable?.Dispose();
            
            ToastDispatcher.Tick -= Dispatching;
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            var service = ServiceLocator.ToastService;
            _disposable?.Dispose();
            _disposable = null;
            _disposable = service.ToastStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(Pushing);
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