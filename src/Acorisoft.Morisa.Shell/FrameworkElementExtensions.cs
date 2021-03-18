using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Acorisoft.Morisa
{
    public static class FrameworkElementExtension
    {
        public static T FindAncestor<T>(this DependencyObject element, int maxDepth = 16) where T : FrameworkElement
        {
            var node = element;
            var parent = element;
            var depth = 0;

            //
            // avoid node null and if parent wasn't T and depth not large than maxDepth 
            // continue work
            while (node != null && !(parent is T) && depth < maxDepth)
            {
                depth++;

                // maybe parent return null
                parent = VisualTreeHelper.GetParent(node);

                // maybe node was null
                node = parent;
            }

            // maybe that cannot return any value just null
            return parent as T;
        }

        public static T As<T>(this DependencyObject element) where T : FrameworkElement
        {
            return element as T;
        }

        public static T FindVisualChild<T>(this DependencyObject element)
        {
            var count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; i++)
            {
                if (VisualTreeHelper.GetChild(element, i) is T value)
                {
                    return value;
                }
            }

            return default;
        }
    }
}
