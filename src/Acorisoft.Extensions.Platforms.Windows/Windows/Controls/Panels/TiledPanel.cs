using System.Windows;
using System.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Panels
{
    public class TiledPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement element in Children)
            {
                element?.Measure(availableSize);
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement element in Children)
            {
                element?.Arrange(new Rect(Xaml.ZeroPoint, finalSize));
            }

            return finalSize;
        }
    }
}