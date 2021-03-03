using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Morisa.Globalization;

namespace Acorisoft.Morisa.ViewModels
{
    public class InspirationElementInsertion : DependencyObject
    {

        public string Key {
            get => (string)GetValue(KeyProperty);
            set => SetValue(KeyProperty, GlobalizationExtension.GetString(value));
        }

        public Type Type { get; set; }

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            "Key",
            typeof(string),
            typeof(InspirationElementInsertion), 
            new PropertyMetadata(null));
    }
}
