using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Acorisoft.Studio.Behaviors
{
    public class LoadCompositionSetBehavior : Behavior<RadioButton>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Checked += OnChecked;  
            base.OnAttached();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}