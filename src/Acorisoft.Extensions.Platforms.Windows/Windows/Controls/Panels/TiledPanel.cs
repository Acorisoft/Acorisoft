using System.Windows;
using System.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Panels
{
    public class TiledPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement element in Children)
            {
                if (element is not null)
                {
                    element.Arrange(new Rect(Xaml.ZeroPoint, finalSize));
                }
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}