using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public abstract class HeadlineControl : ContentControl
    {
        public object Title
        {
            get => (object) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public DataTemplate TitleTemplate
        {
            get => (DataTemplate) GetValue(TitleTemplateProperty);
            set => SetValue(TitleTemplateProperty, value);
        }

        public DataTemplateSelector TitleTemplateSelector
        {
            get => (DataTemplateSelector) GetValue(TitleTemplateSelectorProperty);
            set => SetValue(TitleTemplateSelectorProperty, value);
        }

        public string TitleStringFormat
        {
            get => (string) GetValue(TitleStringFormatProperty);
            set => SetValue(TitleStringFormatProperty, value);
        }


        public object Subtitle
        {
            get => (object) GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public DataTemplate SubtitleTemplate
        {
            get => (DataTemplate) GetValue(SubtitleTemplateProperty);
            set => SetValue(SubtitleTemplateProperty, value);
        }

        public DataTemplateSelector SubtitleTemplateSelector
        {
            get => (DataTemplateSelector) GetValue(SubtitleTemplateSelectorProperty);
            set => SetValue(SubtitleTemplateSelectorProperty, value);
        }

        public string SubtitleStringFormat
        {
            get => (string) GetValue(SubtitleStringFormatProperty);
            set => SetValue(SubtitleStringFormatProperty, value);
        }

        public static readonly DependencyProperty SubtitleStringFormatProperty = DependencyProperty.Register(
            "SubtitleStringFormat",
            typeof(string),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleTemplateSelectorProperty = DependencyProperty.Register(
            "SubtitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleTemplateProperty = DependencyProperty.Register(
            "SubtitleTemplate",
            typeof(DataTemplate),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register(
            "Subtitle",
            typeof(object),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleStringFormatProperty = DependencyProperty.Register(
            "TitleStringFormat",
            typeof(string),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateSelectorProperty = DependencyProperty.Register(
            "TitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(HeadlineControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(HeadlineControl),
            new PropertyMetadata(null));
    }

    public abstract class HeaderedToolBar : HeadlineControl
    {
        
        public object ToolBar
        {
            get => (object) GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }

        public DataTemplate ToolBarTemplate
        {
            get => (DataTemplate) GetValue(ToolBarTemplateProperty);
            set => SetValue(ToolBarTemplateProperty, value);
        }

        public DataTemplateSelector ToolBarTemplateSelector
        {
            get => (DataTemplateSelector) GetValue(ToolBarTemplateSelectorProperty);
            set => SetValue(ToolBarTemplateSelectorProperty, value);
        }

        public string ToolBarStringFormat
        {
            get => (string) GetValue(ToolBarStringFormatProperty);
            set => SetValue(ToolBarStringFormatProperty, value);
        }

        public static readonly DependencyProperty ToolBarStringFormatProperty = DependencyProperty.Register(
            "ToolBarStringFormat",
            typeof(string),
            typeof(HeaderedToolBar),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarTemplateSelectorProperty = DependencyProperty.Register(
            "ToolBarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(HeaderedToolBar),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarTemplateProperty = DependencyProperty.Register(
            "ToolBarTemplate",
            typeof(DataTemplate),
            typeof(HeaderedToolBar),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
            "ToolBar",
            typeof(object),
            typeof(HeaderedToolBar),
            new PropertyMetadata(null));
    }
    
    public class Headline1Control : HeadlineControl
    {
        static Headline1Control()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline1Control),new FrameworkPropertyMetadata(typeof(Headline1Control)));
        }
    }

    public class Headline1HeaderToolbar : HeaderedToolBar
    {
        static Headline1HeaderToolbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline1HeaderToolbar),new FrameworkPropertyMetadata(typeof(Headline1HeaderToolbar)));
        }
    }
    
    public class Headline2HeaderToolbar : HeaderedToolBar
    {
        static Headline2HeaderToolbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline2HeaderToolbar),new FrameworkPropertyMetadata(typeof(Headline2HeaderToolbar)));
        }
    }
    
    public class Headline3HeaderToolbar : HeaderedToolBar
    {
        static Headline3HeaderToolbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline3HeaderToolbar),new FrameworkPropertyMetadata(typeof(Headline3HeaderToolbar)));
        }
    }
    
    public class Headline2Control : HeadlineControl
    {
        static Headline2Control()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline2Control),new FrameworkPropertyMetadata(typeof(Headline2Control)));
        }
    }
    
    public class Headline3Control : HeadlineControl
    {
        static Headline3Control()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Headline3Control),new FrameworkPropertyMetadata(typeof(Headline3Control)));
        }
    }
}