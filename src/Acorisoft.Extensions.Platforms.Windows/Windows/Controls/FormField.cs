using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class FormField : Control
    {
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public DataTemplate TitleTemplate
        {
            get => (DataTemplate)GetValue(TitleTemplateProperty);
            set => SetValue(TitleTemplateProperty, value);
        }

        public DataTemplateSelector TitleTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TitleTemplateSelectorProperty);
            set => SetValue(TitleTemplateSelectorProperty, value);
        }

        public string TitleStringFormat
        {
            get => (string)GetValue(TitleStringFormatProperty);
            set => SetValue(TitleStringFormatProperty, value);
        }

        public static readonly DependencyProperty TitleStringFormatProperty = DependencyProperty.Register(
            "TitleStringFormat",
            typeof(string),
            typeof(FormField),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateSelectorProperty = DependencyProperty.Register(
            "TitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(FormField),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(FormField),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(FormField),
            new PropertyMetadata(null));
    }
}