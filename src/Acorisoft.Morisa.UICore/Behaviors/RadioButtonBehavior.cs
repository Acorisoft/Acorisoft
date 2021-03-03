using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Acorisoft.Morisa.Behaviors
{
    public abstract class RadioButtonBehavior<TRadioButton> : ButtonBehavior<TRadioButton> where TRadioButton : RadioButton
    {
        protected override void OnAttached()
        {
            AssociatedObject.Checked += OnChecked;
            base.OnAttached();
        }

        protected virtual void OnChecked(object sender, RoutedEventArgs e)
        {
           
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Checked -= OnChecked;
            base.OnDetaching();
        }
    }
}
