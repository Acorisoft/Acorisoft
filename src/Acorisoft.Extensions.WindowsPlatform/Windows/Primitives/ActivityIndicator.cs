using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Acorisoft.Extensions.Windows.Services;
using MediatR;
using Splat;

namespace Acorisoft.Extensions.Windows.Primitives
{
    /// <summary>
    /// <see cref="ActivityIndicator"/> 表示一个活动
    /// </summary>
    public class ActivityIndicator : ContentControl, IActivityIndicator
    {
        static ActivityIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivityIndicator),new FrameworkPropertyMetadata(typeof(ActivityIndicator)));
        }
        
        public ActivityIndicator()
        {
            if (!Locator.CurrentMutable.HasRegistration(typeof(IActivityIndicator)))
            {
                Locator.CurrentMutable.RegisterConstant<IActivityIndicator>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IStartActivityRequest>>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IUpdateActivityRequest>>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IEndActivityRequest>>(this);
            }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string), typeof(ActivityIndicator), new PropertyMetadata(string.Empty));

        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));

        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));

        public Task Handle(IStartActivityRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(()=>Dispatcher.Invoke(() =>
            {
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogOpeningEvent
                });
                Description = request.Description;
            }), cancellationToken);
        }

        public Task Handle(IEndActivityRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(()=>Dispatcher.Invoke(() =>
            {
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogClosingEvent
                });
            }), cancellationToken);
        }

        public Task Handle(IUpdateActivityRequest request, CancellationToken cancellationToken)
        {
            
            return Task.Run(()=>Dispatcher.Invoke(() =>
            {
                Description = request.Description;
            }), cancellationToken);
        }


        public string Description
        {
            get => (string) GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
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