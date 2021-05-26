using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Buttons
{
    public class OverlayButton : Button
    {
        public object Overlay
        {
            get => (object)GetValue(OverlayProperty);
            set => SetValue(OverlayProperty, value);
        }

        public DataTemplate OverlayTemplate
        {
            get => (DataTemplate)GetValue(OverlayTemplateProperty);
            set => SetValue(OverlayTemplateProperty, value);
        }

        public DataTemplateSelector OverlayTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(OverlayTemplateSelectorProperty);
            set => SetValue(OverlayTemplateSelectorProperty, value);
        }

        public string OverlayStringFormat
        {
            get => (string)GetValue(OverlayStringFormatProperty);
            set => SetValue(OverlayStringFormatProperty, value);
        }
        
        public static readonly DependencyProperty OverlayStringFormatProperty = DependencyProperty.Register(
            "OverlayStringFormat",
            typeof(string),
            typeof(OverlayButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty OverlayTemplateSelectorProperty = DependencyProperty.Register(
            "OverlayTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(OverlayButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty OverlayTemplateProperty = DependencyProperty.Register(
            "OverlayTemplate",
            typeof(DataTemplate),
            typeof(OverlayButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty OverlayProperty = DependencyProperty.Register(
            "Overlay",
            typeof(object),
            typeof(OverlayButton),
            new PropertyMetadata(null));
    }
}