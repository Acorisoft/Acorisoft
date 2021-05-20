using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Windows.Controls
{
    /// <summary>
    /// <see cref="TiledPanel"/> 表示一个平铺面板，在这个面板下面的所有子级视觉元素都会获得相同的布局大小。
    /// </summary>
    public class TiledPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            // while (LogicalChildren != null && LogicalChildren.MoveNext())
            // {
            //     if (LogicalChildren.Current is FrameworkElement element)
            //     {
            //         element.Arrange(new Rect(new Point(0, 0), finalSize));
            //     }
            // }

            foreach (FrameworkElement element in Children)
            {
                element?.Arrange(new Rect(new Point(0, 0), finalSize));
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }
    }
}