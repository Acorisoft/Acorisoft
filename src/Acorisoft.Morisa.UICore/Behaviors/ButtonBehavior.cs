using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using Microsoft.Xaml.Behaviors;

namespace Acorisoft.Morisa.Behaviors
{
    public abstract class ButtonBehavior<TButton> : Behavior<TButton> where TButton : ButtonBase
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClick;
            base.OnAttached();
        }

        protected virtual void OnClick(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;
            base.OnDetaching();
        }
    }
}
