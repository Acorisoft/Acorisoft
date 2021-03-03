using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Morisa.Controls.Buttons
{
    /// <summary>
    /// </summary>
    public class MuiRadioButton : RadioButton
    {
        static MuiRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MuiRadioButton), new FrameworkPropertyMetadata(typeof(MuiRadioButton)));
        }

        public object Symbol {
            get => (object)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        public DataTemplate SymbolTemplate {
            get => (DataTemplate)GetValue(SymbolTemplateProperty);
            set => SetValue(SymbolTemplateProperty, value);
        }

        public DataTemplateSelector SymbolTemplateSelector {
            get => (DataTemplateSelector)GetValue(SymbolTemplateSelectorProperty);
            set => SetValue(SymbolTemplateSelectorProperty, value);
        }


        public string SymbolStringFormat {
            get => (string)GetValue(SymbolStringFormatProperty);
            set => SetValue(SymbolStringFormatProperty, value);
        }

        public static readonly DependencyProperty SymbolStringFormatProperty = DependencyProperty.Register(
            "SymbolStringFormat",
            typeof(string),
            typeof(MuiRadioButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateSelectorProperty = DependencyProperty.Register(
            "SymbolTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(MuiRadioButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateProperty = DependencyProperty.Register(
            "SymbolTemplate",
            typeof(DataTemplate),
            typeof(MuiRadioButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            "Symbol",
            typeof(object),
            typeof(MuiRadioButton),
            new PropertyMetadata(null));
    }
}
