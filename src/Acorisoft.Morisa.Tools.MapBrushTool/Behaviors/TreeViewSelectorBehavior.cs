using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Acorisoft.Morisa.Tools.ViewModels;

namespace Acorisoft.Morisa.Tools.Behaviors
{
    public abstract class TreeViewSelectorBehavior<T> : Behavior<TreeView>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SelectedItemChanged += OnTreeViewItemSelected;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectedItemChanged -= OnTreeViewItemSelected;
        }

        private void OnTreeViewItemSelected(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if( e.NewValue is T dc)
            {
                OnItemSelected(sender, dc);
            }
        }

        protected virtual void OnItemSelected(object sender,T newValue)
        {

        }
    }
}
