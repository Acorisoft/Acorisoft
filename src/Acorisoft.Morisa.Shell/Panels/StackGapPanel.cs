using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Morisa.Panels
{
    public class StackGapPanel : Panel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var rect = new Rect(new Point(0, 0), arrangeSize);
            var flag = Orientation == Orientation.Horizontal;

            foreach (FrameworkElement element in Children)
            {
                if (flag)
                {
                    rect.Height = IfLessThen(element.ActualHeight, 10, arrangeSize.Height);
                    rect.Width = MathMixins.MinMax(element.ActualWidth, 32, arrangeSize.Width);
                }
                else
                {
                    rect.Height = MathMixins.MinMax(element.ActualHeight, 32, arrangeSize.Height);
                    rect.Width = IfLessThen(element.ActualWidth, 10, arrangeSize.Width);
                }
                element.Arrange(rect);
            }

            foreach (FrameworkElement element in Children)
            {
                rect.Width = element.ActualWidth;
                rect.Height = element.ActualHeight;

                if (flag)
                {
                    //
                    // 横向排列
                    element.Arrange(rect);
                    rect.X += Gap;
                    rect.X += rect.Width;
                }
                else
                {
                    element.Arrange(rect);
                    rect.Y += Gap;
                    rect.Y += rect.Height;
                }
            }


            return arrangeSize;
        }

        protected static double IfLessThen(double value, double min, double threshold)
        {
            if(value < min)
            {
                return threshold;
            }
            return value;
        }


        public double Gap
        {
            get => (double)GetValue(GapProperty);
            set => SetValue(GapProperty, value);
        }


        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(StackGapPanel),
            new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));


        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(StackGapPanel),
            new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    }
}
