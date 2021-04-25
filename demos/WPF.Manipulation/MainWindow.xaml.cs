using System;
using System.Collections.Generic;
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

namespace WPF.Manipulation
{
    public class SimulateTouchDevice : TouchDevice
    {
        public SimulateTouchDevice(int deviceId, Window window) : base(deviceId)
        {
            Window = window;
        }

        public override TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
        {
            return new TouchPointCollection
            {

            };
        }

        /// <summary>
        /// 触摸点
        /// </summary>
        public Point Position { set; get; }

        /// <summary>
        /// 触摸大小
        /// </summary>
        public Size Size { set; get; }

        public void Down()
        {
            TouchAction = TouchAction.Down;

            if (!IsActive)
            {
                SetActiveSource(PresentationSource.FromVisual(Window));

                Activate();
                ReportDown();
            }
            else
            {
                ReportDown();
            }
        }

        public void Move()
        {
            TouchAction = TouchAction.Move;

            ReportMove();
        }

        public void Up()
        {
            TouchAction = TouchAction.Up;

            ReportUp();
            Deactivate();
        }


        private Window Window { get; }

        private TouchAction TouchAction { set; get; }

        public override TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            return new TouchPoint(this, Position, new Rect(Position, Size), TouchAction);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimulateTouchDevice device;
        private bool _isDown;
        public MainWindow()
        {
            InitializeComponent();
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var delta = e.GetPosition((IInputElement)sender);
                var transform = new TranslateTransform(0,(delta.Y / 1080) * Panel.ActualHeight);
                Panel.RenderTransform = transform;
            }
        }

        public bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }

        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            device.Up();
        }

        private void OnTouchMove(object sender, TouchEventArgs e)
        {
            device.Move();
        }

        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            device.Down();
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
        }

        private void OnManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = (IInputElement)sender;
            e.Mode = ManipulationModes.Translate;
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
        }

        private void OnManipulationStated(object sender, ManipulationStartedEventArgs e)
        {

        }
    }
}
