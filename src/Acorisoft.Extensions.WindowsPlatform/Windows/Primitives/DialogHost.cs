using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using Acorisoft.Extensions.Windows.Services;
using ReactiveUI;

namespace Acorisoft.Extensions.Windows.Primitives
{
    public class DialogHost: ContentControl
    {

        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DialogHost));
        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DialogHost));
        public static readonly DependencyProperty DialogProperty = DependencyProperty.Register("Dialog", typeof(object), typeof(DialogHost), new PropertyMetadata(default(object), OnDialogChanged));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(DialogHost), new PropertyMetadata(Xaml.False, OnIsOpenChanged));
        
        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost),new FrameworkPropertyMetadata(typeof(DialogHost)));
        }
        
        
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

        private readonly CompositeDisposable _disposable;

        public DialogHost()
        {
            _disposable = new CompositeDisposable();
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedImpl;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        //
        // IDialogService / IViewService Interfaces
        //
        //--------------------------------------------------------------------------------------------------------------
        
        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {
            if (!_disposable.IsDisposed)
            {
                _disposable.Dispose();
            }
            OnUnloaded(sender, e);
        }

        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
            var service = ServiceLocator.DialogService;
            var disposable1 = service.DialogCloseStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(x =>
            {
                IsOpen = false;
            });
            var disposable2 = service.DialogOpenStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(x =>
            {
                IsOpen = true;
            });
            var disposable3 = service.DialogUpdateStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(x =>
            {
                Dialog = x;
            });
            
            _disposable.Add(disposable1);
            _disposable.Add(disposable2);
            _disposable.Add(disposable3);

            OnLoaded(sender, e);
        }

        //--------------------------------------------------------------------------------------------------------------
        //
        // IDialogService / IViewService Interfaces
        //
        //--------------------------------------------------------------------------------------------------------------

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, Xaml.Box(value));
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
        //
        // IDisposable IDialogHostCore.SubscribeDialogOpening(IObservable<Unit> observable)
        // {
        //     return observable?.ObserveOn(RxApp.MainThreadScheduler)
        //         .Subscribe(x =>
        //         {
        //             IsOpen = true;
        //         });
        // }
        //
        // IDisposable IDialogHostCore.SubscribeDialogClosing(IObservable<Unit> observable)
        // {
        //     return observable?.ObserveOn(RxApp.MainThreadScheduler)
        //         .Subscribe(x =>
        //         {
        //             IsOpen = false;
        //         });
        // }
        //
        // IDisposable IDialogHostCore.SubscribeDialogChanged(IObservable<object> observable)
        // {
        //     return observable?.ObserveOn(RxApp.MainThreadScheduler)
        //         .Subscribe(x =>
        //         {
        //             Dialog = x;
        //         });
        // }
    }
}