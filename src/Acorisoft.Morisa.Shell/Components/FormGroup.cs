using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class FormGroup : ItemsControl
    {
        static FormGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormGroup), new FrameworkPropertyMetadata(typeof(FormGroup)));
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

        public object Tools
        {
            get => (object)GetValue(ToolsProperty);
            set => SetValue(ToolsProperty, value);
        }

        public static readonly DependencyProperty ToolsProperty = DependencyProperty.Register(
            "Tools",
            typeof(object),
            typeof(FormGroup), 
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(FormGroup),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register(
            "Control",
            typeof(object),
            typeof(FormGroup),
            new FrameworkPropertyMetadata(null));
    }
}
