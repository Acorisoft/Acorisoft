using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Windows.Panels
{
    public class TiledPanel : Panel
    {
        private static readonly Point Zero = new Point(0, 0);

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
                element?.Arrange(new Rect(Zero, finalSize));
            }

            return finalSize;
        }
    }
}