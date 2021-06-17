using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Acorisoft.Extensions.Windows.Services;
using MediatR;
using Splat;

namespace Acorisoft.Extensions.Windows.Primitives
{
    /// <summary>
    /// <see cref="ActivityIndicator"/> 表示一个活动指示器。
    /// </summary>
    [DefaultProperty("Content")]
    [ContentProperty("Content")]
    public class ActivityIndicator : ContentControl, IActivityIndicator
    {
        

        //--------------------------------------------------------------------------------------------------------------
        //
        // Static Fields
        //
        //--------------------------------------------------------------------------------------------------------------

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string), typeof(ActivityIndicator), new PropertyMetadata(string.Empty));

        public static readonly RoutedEvent DialogOpeningEvent = EventManager.RegisterRoutedEvent("DialogOpening",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));

        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent("DialogClosing",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ActivityIndicator));
        
        
        //--------------------------------------------------------------------------------------------------------------
        //
        // Static Constructors
        //
        //--------------------------------------------------------------------------------------------------------------
        static ActivityIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivityIndicator),
                new FrameworkPropertyMetadata(typeof(ActivityIndicator)));
        }

        //--------------------------------------------------------------------------------------------------------------
        //
        // Constructors
        //
        //--------------------------------------------------------------------------------------------------------------
        public ActivityIndicator()
        {
            if (!Locator.CurrentMutable.HasRegistration(typeof(IActivityIndicator)))
            {
                Locator.CurrentMutable.RegisterConstant<IActivityIndicator>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IStartActivityRequest>>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IUpdateActivityRequest>>(this);
                Locator.CurrentMutable.RegisterConstant<INotificationHandler<IEndActivityRequest>>(this);
            }

            this.Unloaded += InternUnloaded;
        }

        private void InternUnloaded(object sender, RoutedEventArgs e)
        {
            if (!Locator.CurrentMutable.HasRegistration(typeof(IActivityIndicator)))
            {
                Locator.CurrentMutable.UnregisterCurrent<IActivityIndicator>();
                Locator.CurrentMutable.UnregisterCurrent<INotificationHandler<IStartActivityRequest>>();
                Locator.CurrentMutable.UnregisterCurrent<INotificationHandler<IUpdateActivityRequest>>();
                Locator.CurrentMutable.UnregisterCurrent<INotificationHandler<IEndActivityRequest>>();
            }

            this.Unloaded -= InternUnloaded;
        }



        //--------------------------------------------------------------------------------------------------------------
        //
        // Public Methods

        //
        //--------------------------------------------------------------------------------------------------------------

        #region Public Methods

        

        public Task Handle(IStartActivityRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(() => Dispatcher.Invoke(() =>
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
            return Task.Run(() => Dispatcher.Invoke(() =>
            {
                RaiseEvent(new RoutedEventArgs
                {
                    RoutedEvent = DialogClosingEvent
                });
            }), cancellationToken);
        }

        public Task Handle(IUpdateActivityRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(() => Dispatcher.Invoke(() => { Description = request.Description; }), cancellationToken);
        }


        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
        //
        // Properties
        //
        //--------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 获取或设置当前的活动主题
        /// </summary>
        public string Description
        {
            get => (string) GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }


        //--------------------------------------------------------------------------------------------------------------
        //
        // Events
        //
        //--------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 当活动开启时，触发该事件
        /// </summary>
        public event RoutedEventHandler DialogOpening
        {
            add => AddHandler(DialogOpeningEvent, value);
            remove => RemoveHandler(DialogOpeningEvent, value);
        }

        /// <summary>
        /// 当活动关闭时，触发该事件
        /// </summary>
        public event RoutedEventHandler DialogClosing
        {
            add => AddHandler(DialogClosingEvent, value);
            remove => RemoveHandler(DialogClosingEvent, value);
        }
    }
}