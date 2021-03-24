using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Acorisoft.Morisa.Tools.ViewModels;

namespace Acorisoft.Morisa.Tools.Behaviors
{
    public class ConductTagBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter ||e.SystemKey == Key.Enter)
            {
                if (AssociatedObject.DataContext is NewBrushSetDialogStep2ViewModel vm && !string.IsNullOrEmpty(vm.Tag))
                {
                    vm.Tags.Add(vm.Tag);
                    vm.Tag = string.Empty;
                }
            }
        }
    }
}
