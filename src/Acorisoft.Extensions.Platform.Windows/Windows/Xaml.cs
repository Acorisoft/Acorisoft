using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.Threadings;
using Acorisoft.Extensions.Windows.ViewModels;
using Splat;

namespace Acorisoft.Extensions.Windows
{
    public static class Xaml
    {
        public static TElement FindAncestor<TElement>(this FrameworkElement element) where TElement : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(element) ?? throw new ArgumentNullException("element");
            while (parent is not null)
            {
                // ReSharper disable once MergeCastWithTypeCheck
                if (parent is TElement)
                {
                    return (TElement) parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return default(TElement);
        }
    }
}