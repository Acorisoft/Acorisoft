using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Morisa.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class FormGroup : Control,IAddChild
    {
        static FormGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormGroup), new FrameworkPropertyMetadata(typeof(FormGroup)));
            ChildrenPropertyKey = DependencyProperty.RegisterReadOnly(
                "Children",
                typeof(ObservableCollection<object>),
                typeof(FormGroup),
                new FrameworkPropertyMetadata(new ObservableCollection<object>()));
            ChildrenProperty = ChildrenPropertyKey.DependencyProperty;
        }

        public object Control
        {
            get => (object)GetValue(ControlProperty);
            set => SetValue(ControlProperty, value);
        }

        public object Header
        {
            get => (object)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public ObservableCollection<object> Children
        {
            get => (ObservableCollection<object>)GetValue(ChildrenProperty);
            private set => SetValue(ChildrenPropertyKey, value);
        }



        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(FormGroup),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyPropertyKey ChildrenPropertyKey;
        public static readonly DependencyProperty ChildrenProperty;

        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register(
            "Control",
            typeof(object),
            typeof(FormGroup),
            new FrameworkPropertyMetadata(null));

        void IAddChild.AddChild(object value)
        {
            Children.Add(value);
        }

        void IAddChild.AddText(string text)
        {
            Children.Add(text);
        }
    }
}
