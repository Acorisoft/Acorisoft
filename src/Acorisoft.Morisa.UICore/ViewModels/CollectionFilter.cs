using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Acorisoft.Morisa.ViewModels
{
    public class CollectionFilter : DependencyObject
    {
        public virtual bool Predicate(object element)
        {
            return true;
        }


        public string Key {
            get => (string)GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            "Key",
            typeof(string),
            typeof(CollectionFilter), 
            new PropertyMetadata(null));

    }
}
