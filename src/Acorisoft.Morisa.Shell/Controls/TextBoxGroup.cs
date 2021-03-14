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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Morisa.Controls
{
    /// <summary>
    /// <see cref="TextBoxGroup"/> 表示一个文本框控件组。
    /// </summary>
    [ContentProperty("Content")]
    public class TextBoxGroup : ContentControl, IControlGroupChildren
    {
        static TextBoxGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxGroup) , new FrameworkPropertyMetadata(typeof(TextBoxGroup)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public double UniteHeaderWidth { get; set; }

        public object Header {
            get => (object)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty , value);
        }

        public DataTemplate HeaderTemplate {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty , value);
        }

        public DataTemplateSelector HeaderTemplateSelector {
            get => (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty);
            set => SetValue(HeaderTemplateSelectorProperty , value);
        }


        public string HeaderStringFormat {
            get => (string)GetValue(HeaderStringFormatProperty);
            set => SetValue(HeaderStringFormatProperty , value);
        }


        public object Symbol {
            get => (object)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty , value);
        }

        public DataTemplate SymbolTemplate {
            get => (DataTemplate)GetValue(SymbolTemplateProperty);
            set => SetValue(SymbolTemplateProperty , value);
        }

        public DataTemplateSelector SymbolTemplateSelector {
            get => (DataTemplateSelector)GetValue(SymbolTemplateSelectorProperty);
            set => SetValue(SymbolTemplateSelectorProperty , value);
        }


        public string SymbolStringFormat {
            get => (string)GetValue(SymbolStringFormatProperty);
            set => SetValue(SymbolStringFormatProperty , value);
        }

        public FontFamily HeaderFontFamily {
            get => (FontFamily)GetValue(HeaderFontFamilyProperty);
            set => SetValue(HeaderFontFamilyProperty , value);
        }


        public FontStyle HeaderFontStyle {
            get => (FontStyle)GetValue(HeaderFontStyleProperty);
            set => SetValue(HeaderFontStyleProperty , value);
        }


        public FontWeight HeaderFontWeight {
            get => (FontWeight)GetValue(HeaderFontWeightProperty);
            set => SetValue(HeaderFontWeightProperty , value);
        }

        public FontStretch HeaderFontStretch {
            get => (FontStretch)GetValue(HeaderFontStretchProperty);
            set => SetValue(HeaderFontStretchProperty , value);
        }


        public double HeaderFontSize {
            get => (double)GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty , value);
        }


        public Brush HeaderForeground {
            get => (Brush)GetValue(HeaderForegroundProperty);
            set => SetValue(HeaderForegroundProperty , value);
        }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register(
            "HeaderForeground",
            typeof(Brush),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register(
            "HeaderFontSize",
            typeof(double),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderFontStretchProperty = DependencyProperty.Register(
            "HeaderFontStretch",
            typeof(FontStretch),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register(
            "HeaderFontWeight",
            typeof(FontWeight),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderFontStyleProperty = DependencyProperty.Register(
            "HeaderFontStyle",
            typeof(FontStyle),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderFontFamilyProperty = DependencyProperty.Register(
            "HeaderFontFamily",
            typeof(FontFamily),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));


        public static readonly DependencyProperty HeaderStringFormatProperty = DependencyProperty.Register(
            "HeaderStringFormat",
            typeof(string),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register(
            "HeaderTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate",
            typeof(DataTemplate),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SymbolStringFormatProperty = DependencyProperty.Register(
            "SymbolStringFormat",
            typeof(string),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateSelectorProperty = DependencyProperty.Register(
            "SymbolTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateProperty = DependencyProperty.Register(
            "SymbolTemplate",
            typeof(DataTemplate),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            "Symbol",
            typeof(object),
            typeof(TextBoxGroup),
            new PropertyMetadata(null));
    }
}
