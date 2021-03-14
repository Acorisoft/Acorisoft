using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Morisa.Panels
{
    public class StackGapPanel : StackPanel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var rect = new Rect(new Point(0, 0), arrangeSize);
            var flag = Orientation == Orientation.Horizontal;

            base.ArrangeOverride(arrangeSize);

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

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(StackGapPanel),
            new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    }
}
