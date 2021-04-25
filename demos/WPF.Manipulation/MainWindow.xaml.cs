using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF.Manipulation
{
    public abstract class MouseGestureRecognition
    {
        protected enum Behavior
        {
            NoAction,
            Collapsing,
            Expansing
        }

        protected class Pointer
        {
            private Point _Last,_Current;
            private int _Time;

            public void Set(Point point)
            {
                _Last = _Current;
                _Current = point;
                _Time++;
            }

            public void Unset() => _Time = 0;

            public Point Last => _Last;
            public Point Current => _Current;

            public bool IsEnable => _Time > 2;
        }

        private double _Delta;
        private DispatcherTimer _Timer;
        private FrameworkElement _Element;
        private readonly Pointer _Pointer;
        private bool _IsDragging;

        public MouseGestureRecognition()
        {

            _Delta = 0d;
            _Pointer = new Pointer();
            TargetBehavior = Behavior.NoAction;
        }

        public void SetSampler(DispatcherTimer timer)
        {
            if (_Timer is null)
            {
                _Timer = timer ?? throw new ArgumentNullException(nameof(timer));
                _Timer.Tick += Sampling;
                _Timer.Interval = TimeSpan.FromMilliseconds(8);
                _Timer.Start();
            }
        }

        public void SetInputElement(FrameworkElement element) => _Element = element;

        void Sampling(object sender, EventArgs e)
        {
            var pressed = Mouse.LeftButton == MouseButtonState.Pressed || Mouse.RightButton == MouseButtonState.Pressed;

            //
            //
            PerformanceDragging(pressed);

            //
            // 
            if (!_IsDragging && TargetBehavior != Behavior.NoAction)
            {
                PerformanceBehavior();
            }

        }

        protected void PerformanceDragging(bool pressed)
        {
            if (pressed)
            {
                _Pointer.Set(Mouse.GetPosition(_Element));
                _IsDragging = true;

                if (_Pointer.IsEnable)
                {
                    _Delta += 1 * React(_Pointer);
                    _Delta = Math.Clamp(_Delta, 0d, 1d);
                    Dragging?.Invoke(Delta);
                }
            }
            else
            {
                if (_Delta > 0 && _Delta < .73d)
                {
                    TargetBehavior = Behavior.Collapsing;
                }
                else if (_Delta > .73 && _Delta < 1d)
                {
                    TargetBehavior = pressed ? Behavior.NoAction : Behavior.Expansing;
                }
                else
                {
                    TargetBehavior = Behavior.NoAction;
                }

                _IsDragging = false;
                _Pointer.Unset();
            }
        }

        protected abstract double React(Pointer pointer);

        protected void PerformanceBehavior()
        {
            if (!_IsDragging)
            {
                switch (TargetBehavior)
                {
                    case Behavior.Collapsing:
                        _Delta -= 0.02;
                        _Delta = Math.Clamp(_Delta, 0d, 1d);
                        Collapsing?.Invoke(Delta);
                        break;
                    case Behavior.Expansing:
                        _Delta += 0.02;
                        _Delta = Math.Clamp(_Delta, 0d, 1d);
                        Expanding?.Invoke(Delta);
                        break;
                }
            }
        }

        /// <summary>
        /// 获取或设置当前目标的状态。
        /// </summary>
        protected Behavior TargetBehavior { get; set; }

        protected FrameworkElement Element => _Element;

        /// <summary>
        /// 获取当前手势的导数
        /// </summary>
        public double Delta { get => _Delta; }

        public event Action<double> Collapsing;
        public event Action<double> Expanding;
        public event Action<double> Dragging;
    }

    public sealed class SweapDownRecognition : MouseGestureRecognition
    {
        protected override double React(Pointer pointer)
        {
            return (pointer.Current.Y - pointer.Last.Y) / 192;
        }
    }

    public sealed class SweapLeftRecognition : MouseGestureRecognition
    {
        protected override double React(Pointer pointer)
        {
            return (pointer.Current.X - pointer.Last.X) / 192;
        }
    }

    public class IxContentControl : ContentControl
    {
        protected abstract class TranslateTransformer
        {
            public abstract void Transform(FrameworkElement parent, FrameworkElement element, double delta);
        }

        protected sealed class Top2BottomTransformer : TranslateTransformer
        {
            public override void Transform(FrameworkElement parent,FrameworkElement element, double delta)
            {
                var height = element.ActualHeight;
                var transform = new TranslateTransform(0, -height + height * delta);
                element.RenderTransform = transform;
            }
        }

        protected sealed class Bottom2TopTransformer : TranslateTransformer
        {
            public override void Transform(FrameworkElement parent, FrameworkElement element, double delta)
            {
                var height = element.ActualHeight;
                var transform = new TranslateTransform(0, height - height * delta);
                element.RenderTransform = transform;
            }
        }

        protected sealed class Left2RightTransformer : TranslateTransformer
        {
            public override void Transform(FrameworkElement parent, FrameworkElement element, double delta)
            {
                var width = element.ActualWidth;
                var transform = new TranslateTransform(-width + delta * width,0);
                element.RenderTransform = transform;
            }
        }

        protected sealed class Right2LeftTransformer : TranslateTransformer
        {
            public override void Transform(FrameworkElement parent, FrameworkElement element, double delta)
            {
                var width = element.ActualHeight;
                var transform = new TranslateTransform(parent.ActualWidth - width * delta, 0);
                element.RenderTransform = transform;
            }
        }

        private TranslateTransformer _Transformer;
        private ContentPresenter _Presenter;

        public IxContentControl()
        {
            _Transformer = new Top2BottomTransformer();
            this.SizeChanged += OnSizeChanged;
        }

        protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_Presenter is not null)
            {
                _Transformer?.Transform(this, _Presenter, 0);
            }
        }

        public override void OnApplyTemplate()
        {
            var count = VisualTreeHelper.GetChildrenCount(this);
            for(int i = 0;i < count; i++)
            {
                if(VisualTreeHelper.GetChild(this,i) is ContentPresenter presenter)
                {
                    _Presenter = presenter;
                    _Transformer?.Transform(this, presenter, 0);
                }
            }
            base.OnApplyTemplate();
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            if (_Presenter is not null)
            {
                _Transformer?.Transform(this, _Presenter, 0);
            }
            base.OnChildDesiredSizeChanged(child);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (_Presenter is not null)
            {
                _Transformer?.Transform(this, _Presenter, 0);
            }
            base.OnContentChanged(oldContent, newContent);
        }

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var direction = (ExpandDirection)e.NewValue;
            var tcc = (IxContentControl)d;
            switch (direction)
            {
                case ExpandDirection.Down:
                    tcc._Transformer = new Top2BottomTransformer();
                    break;
                case ExpandDirection.Up:
                    tcc._Transformer = new Bottom2TopTransformer();
                    break;
                case ExpandDirection.Left:
                    tcc._Transformer = new Right2LeftTransformer();
                    break;
                case ExpandDirection.Right:
                    tcc._Transformer = new Left2RightTransformer();
                    break;
            }
        }

        private static void OnDeltaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var delta = (double)e.NewValue;
            var parent = (IxContentControl)d;
            var element = parent._Presenter;
            var transformer = parent._Transformer;
            if(element is not null)
            {
                transformer?.Transform(parent, element, delta);
            }
        }


        public ExpandDirection Direction
        {
            get => (ExpandDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(ExpandDirection),
            typeof(IxContentControl),
            new PropertyMetadata(ExpandDirection.Down, OnDirectionChanged));


        public double Delta
        {
            get => (double)GetValue(DeltaProperty);
            set => SetValue(DeltaProperty, value);
        }

        public static readonly DependencyProperty DeltaProperty = DependencyProperty.Register(
            "Delta",
            typeof(double),
            typeof(IxContentControl),
            new PropertyMetadata(0d, OnDeltaChanged));

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _Timer;
        private readonly MouseGestureRecognition     _Recognition;

        public MainWindow()
        {
            _Timer = new DispatcherTimer(DispatcherPriority.Render, Dispatcher);
            _Recognition = new SweapDownRecognition();
            _Recognition.SetInputElement(this);
            _Recognition.SetSampler(_Timer);
            _Recognition.Expanding += OnPerformancePosition;
            _Recognition.Collapsing += OnPerformancePosition;
            _Recognition.Dragging += OnPerformancePosition;
            InitializeComponent();
        }

        private void OnPerformancePosition(double delta)
        {
            Panel.Delta = delta;
        }
    }
}
