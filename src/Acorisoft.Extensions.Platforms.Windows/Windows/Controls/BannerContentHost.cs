using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    /// <summary>
    /// <see cref="BannerContentHost"/>
    /// </summary>
    public class BannerContentHost : ContentControl
    {
        static BannerContentHost()
        {
            OverridesDefaultStyleProperty.OverrideMetadata(typeof(BannerContentHost), new FrameworkPropertyMetadata(typeof(BannerContentHost)));
        }

        public double BannerHeight
        {
            get => (double) GetValue(BannerHeightProperty); 
            set => SetValue(BannerHeightProperty, value);
        }
        public double BannerWidth
        {
            get => (double) GetValue(BannerWidthProperty); 
            set => SetValue(BannerWidthProperty, value);
        }
        
        public object Toolbar
        {
            get => (object)GetValue(ToolbarProperty);
            set => SetValue(ToolbarProperty, value);
        }

        public DataTemplate ToolbarTemplate
        {
            get => (DataTemplate)GetValue(ToolbarTemplateProperty);
            set => SetValue(ToolbarTemplateProperty, value);
        }

        public DataTemplateSelector ToolbarTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ToolbarTemplateSelectorProperty);
            set => SetValue(ToolbarTemplateSelectorProperty, value);
        }

        public string ToolbarStringFormat
        {
            get => (string)GetValue(ToolbarStringFormatProperty);
            set => SetValue(ToolbarStringFormatProperty, value);
        }

        public object Banner
        {
            get => (object)GetValue(BannerProperty);
            set => SetValue(BannerProperty, value);
        }

        public DataTemplate BannerTemplate
        {
            get => (DataTemplate)GetValue(BannerTemplateProperty);
            set => SetValue(BannerTemplateProperty, value);
        }

        public DataTemplateSelector BannerTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(BannerTemplateSelectorProperty);
            set => SetValue(BannerTemplateSelectorProperty, value);
        }

        public string BannerStringFormat
        {
            get => (string)GetValue(BannerStringFormatProperty);
            set => SetValue(BannerStringFormatProperty, value);
        }

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly DependencyProperty BannerWidthProperty = DependencyProperty.Register(
            "BannerWidth", typeof(double), typeof(BannerContentHost), new PropertyMetadata(default(double)));

        
        public static readonly DependencyProperty BannerHeightProperty = DependencyProperty.Register(
            "BannerHeight", typeof(double), typeof(BannerContentHost), new PropertyMetadata(default(double)));

        
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Brush),
            typeof(BannerContentHost), 
            new PropertyMetadata(null));


        public static readonly DependencyProperty BannerStringFormatProperty = DependencyProperty.Register(
            "BannerStringFormat",
            typeof(string),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty BannerTemplateSelectorProperty = DependencyProperty.Register(
            "BannerTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty BannerTemplateProperty = DependencyProperty.Register(
            "BannerTemplate",
            typeof(DataTemplate),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty BannerProperty = DependencyProperty.Register(
            "Banner",
            typeof(object),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolbarStringFormatProperty = DependencyProperty.Register(
            "ToolbarStringFormat",
            typeof(string),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolbarTemplateSelectorProperty = DependencyProperty.Register(
            "ToolbarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolbarTemplateProperty = DependencyProperty.Register(
            "ToolbarTemplate",
            typeof(DataTemplate),
            typeof(BannerContentHost),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolbarProperty = DependencyProperty.Register(
            "Toolbar",
            typeof(object),
            typeof(BannerContentHost),
            new PropertyMetadata(null));
    }
}