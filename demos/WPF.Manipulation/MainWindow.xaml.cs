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
        private Pointer _Pointer;
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
                _Timer.Interval = TimeSpan.FromMilliseconds(10);
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
                PerformanceBehavior(pressed);
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
                    _Delta += 1.46 * React(_Pointer);
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

        protected void PerformanceBehavior(bool pressed)
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _Timer;
        private readonly SweapLeftRecognition _Recognition;
        public MainWindow()
        {
            _Timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher);
            _Recognition = new SweapLeftRecognition();
            _Recognition.SetInputElement(this);
            _Recognition.SetSampler(_Timer);
            _Recognition.Expanding += OnPerformancePosition;
            _Recognition.Collapsing += OnPerformancePosition;
            _Recognition.Dragging += OnPerformancePosition;
            InitializeComponent();
        }

        private void OnPerformancePosition(double delta)
        {
            Panel.RenderTransform = new TranslateTransform(-360 + delta * 360, 0);
        }
    }
}
