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

namespace Acorisoft.Morisa.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class GalleryViewLayout : Panel
    {
        private static readonly Point Zero = new Point(0,0);

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children != null || Children.Count > 3)
            {
                var element1 = Children[0]  as FrameworkElement;
                var element2 = Children[1]  as FrameworkElement;
                var element3 = Children[2]  as FrameworkElement;
                var element4 = Children[3]  as FrameworkElement;

                //
                // 产生预期的大小。
                element1.Arrange(new Rect(Zero, finalSize));
                element2.Arrange(new Rect(Zero, finalSize));
                element3.Arrange(new Rect(Zero, finalSize));
                element4.Arrange(new Rect(Zero, finalSize));

                var h1 = MinMax(element1.DesiredSize.Height,24,96);
                var h2 = MinMax(element2.DesiredSize.Height,24,96);
                var h3 = MinMax(element3.DesiredSize.Height,0,double.PositiveInfinity);
                var h4 = MinMax(finalSize.Height - h1 - h2 - h3,100,double.PositiveInfinity);

                element1.Arrange(new Rect(Zero, new Size(finalSize.Width, h1)));
                element2.Arrange(new Rect(new Point(0, h1), new Size(finalSize.Width, h2)));
                element3.Arrange(new Rect(new Point(0, h1 + h2), new Size(finalSize.Width, h3)));
                element4.Arrange(new Rect(new Point(0, h1 + h2 + h3), new Size(finalSize.Width, h4)));
            }

            return base.ArrangeOverride(finalSize);
        }

        protected static double MinMax(double val, double min, double max)
        {
            return Math.Max(min, Math.Min(val, max));
        }
    }

    /// <summary>
    /// 表示画廊视图控件。
    /// </summary>
    public class GalleryView : ContentControl
    {
        static GalleryView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GalleryView), new FrameworkPropertyMetadata(typeof(GalleryView)));
        }

        public object MainTitle {
            get => (object)GetValue(MainTitleProperty);
            set => SetValue(MainTitleProperty, value);
        }

        public DataTemplate MainTitleTemplate {
            get => (DataTemplate)GetValue(MainTitleTemplateProperty);
            set => SetValue(MainTitleTemplateProperty, value);
        }

        public DataTemplateSelector MainTitleTemplateSelector {
            get => (DataTemplateSelector)GetValue(MainTitleTemplateSelectorProperty);
            set => SetValue(MainTitleTemplateSelectorProperty, value);
        }


        public string MainTitleStringFormat {
            get => (string)GetValue(MainTitleStringFormatProperty);
            set => SetValue(MainTitleStringFormatProperty, value);
        }

        public object SubTitle {
            get => (object)GetValue(SubTitleProperty);
            set => SetValue(SubTitleProperty, value);
        }

        public DataTemplate SubTitleTemplate {
            get => (DataTemplate)GetValue(SubTitleTemplateProperty);
            set => SetValue(SubTitleTemplateProperty, value);
        }

        public DataTemplateSelector SubTitleTemplateSelector {
            get => (DataTemplateSelector)GetValue(SubTitleTemplateSelectorProperty);
            set => SetValue(SubTitleTemplateSelectorProperty, value);
        }


        public string SubTitleStringFormat {
            get => (string)GetValue(SubTitleStringFormatProperty);
            set => SetValue(SubTitleStringFormatProperty, value);
        }

        public FontFamily MainTitleFontFamily {
            get => (FontFamily)GetValue(MainTitleFontFamilyProperty);
            set => SetValue(MainTitleFontFamilyProperty, value);
        }


        public FontStyle MainTitleFontStyle {
            get => (FontStyle)GetValue(MainTitleFontStyleProperty);
            set => SetValue(MainTitleFontStyleProperty, value);
        }


        public FontWeight MainTitleFontWeight {
            get => (FontWeight)GetValue(MainTitleFontWeightProperty);
            set => SetValue(MainTitleFontWeightProperty, value);
        }

        public FontStretch MainTitleFontStretch {
            get => (FontStretch)GetValue(MainTitleFontStretchProperty);
            set => SetValue(MainTitleFontStretchProperty, value);
        }


        public double MainTitleFontSize {
            get => (double)GetValue(MainTitleFontSizeProperty);
            set => SetValue(MainTitleFontSizeProperty, value);
        }


        public Brush MainTitleForeground {
            get => (Brush)GetValue(MainTitleForegroundProperty);
            set => SetValue(MainTitleForegroundProperty, value);
        }


        public FontFamily SubTitleFontFamily {
            get => (FontFamily)GetValue(SubTitleFontFamilyProperty);
            set => SetValue(SubTitleFontFamilyProperty, value);
        }


        public FontStyle SubTitleFontStyle {
            get => (FontStyle)GetValue(SubTitleFontStyleProperty);
            set => SetValue(SubTitleFontStyleProperty, value);
        }


        public FontWeight SubTitleFontWeight {
            get => (FontWeight)GetValue(SubTitleFontWeightProperty);
            set => SetValue(SubTitleFontWeightProperty, value);
        }

        public FontStretch SubTitleFontStretch {
            get => (FontStretch)GetValue(SubTitleFontStretchProperty);
            set => SetValue(SubTitleFontStretchProperty, value);
        }


        public double SubTitleFontSize {
            get => (double)GetValue(SubTitleFontSizeProperty);
            set => SetValue(SubTitleFontSizeProperty, value);
        }


        public Brush SubTitleForeground {
            get => (Brush)GetValue(SubTitleForegroundProperty);
            set => SetValue(SubTitleForegroundProperty, value);
        }

        public object ToolInMainTitle {
            get => (object)GetValue(ToolInMainTitleProperty);
            set => SetValue(ToolInMainTitleProperty, value);
        }

        public DataTemplate ToolInMainTitleTemplate {
            get => (DataTemplate)GetValue(ToolInMainTitleTemplateProperty);
            set => SetValue(ToolInMainTitleTemplateProperty, value);
        }

        public DataTemplateSelector ToolInMainTitleTemplateSelector {
            get => (DataTemplateSelector)GetValue(ToolInMainTitleTemplateSelectorProperty);
            set => SetValue(ToolInMainTitleTemplateSelectorProperty, value);
        }


        public string ToolInMainTitleStringFormat {
            get => (string)GetValue(ToolInMainTitleStringFormatProperty);
            set => SetValue(ToolInMainTitleStringFormatProperty, value);
        }

        public object ToolInSubTitle {
            get => (object)GetValue(ToolInSubTitleProperty);
            set => SetValue(ToolInSubTitleProperty, value);
        }

        public DataTemplate ToolInSubTitleTemplate {
            get => (DataTemplate)GetValue(ToolInSubTitleTemplateProperty);
            set => SetValue(ToolInSubTitleTemplateProperty, value);
        }

        public DataTemplateSelector ToolInSubTitleTemplateSelector {
            get => (DataTemplateSelector)GetValue(ToolInSubTitleTemplateSelectorProperty);
            set => SetValue(ToolInSubTitleTemplateSelectorProperty, value);
        }


        public string ToolInSubTitleStringFormat {
            get => (string)GetValue(ToolInSubTitleStringFormatProperty);
            set => SetValue(ToolInSubTitleStringFormatProperty, value);
        }

        public object ToolBar {
            get => (object)GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }

        public DataTemplate ToolBarTemplate {
            get => (DataTemplate)GetValue(ToolBarTemplateProperty);
            set => SetValue(ToolBarTemplateProperty, value);
        }

        public DataTemplateSelector ToolBarTemplateSelector {
            get => (DataTemplateSelector)GetValue(ToolBarTemplateSelectorProperty);
            set => SetValue(ToolBarTemplateSelectorProperty, value);
        }


        public string ToolBarStringFormat {
            get => (string)GetValue(ToolBarStringFormatProperty);
            set => SetValue(ToolBarStringFormatProperty, value);
        }

        public static readonly DependencyProperty ToolBarStringFormatProperty = DependencyProperty.Register(
            "ToolBarStringFormat",
            typeof(string),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarTemplateSelectorProperty = DependencyProperty.Register(
            "ToolBarTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarTemplateProperty = DependencyProperty.Register(
            "ToolBarTemplate",
            typeof(DataTemplate),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
            "ToolBar",
            typeof(object),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ToolInSubTitleStringFormatProperty = DependencyProperty.Register(
            "ToolInSubTitleStringFormat",
            typeof(string),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInSubTitleTemplateSelectorProperty = DependencyProperty.Register(
            "ToolInSubTitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInSubTitleTemplateProperty = DependencyProperty.Register(
            "ToolInSubTitleTemplate",
            typeof(DataTemplate),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInSubTitleProperty = DependencyProperty.Register(
            "ToolInSubTitle",
            typeof(object),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ToolInMainTitleStringFormatProperty = DependencyProperty.Register(
            "ToolInMainTitleStringFormat",
            typeof(string),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInMainTitleTemplateSelectorProperty = DependencyProperty.Register(
            "ToolInMainTitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInMainTitleTemplateProperty = DependencyProperty.Register(
            "ToolInMainTitleTemplate",
            typeof(DataTemplate),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolInMainTitleProperty = DependencyProperty.Register(
            "ToolInMainTitle",
            typeof(object),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SubTitleForegroundProperty = DependencyProperty.Register(
            "SubTitleForeground",
            typeof(Brush),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty SubTitleFontSizeProperty = DependencyProperty.Register(
            "SubTitleFontSize",
            typeof(double),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty SubTitleFontStretchProperty = DependencyProperty.Register(
            "SubTitleFontStretch",
            typeof(FontStretch),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubTitleFontWeightProperty = DependencyProperty.Register(
            "SubTitleFontWeight",
            typeof(FontWeight),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty SubTitleFontStyleProperty = DependencyProperty.Register(
            "SubTitleFontStyle",
            typeof(FontStyle),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty SubTitleFontFamilyProperty = DependencyProperty.Register(
            "SubTitleFontFamily",
            typeof(FontFamily),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty MainTitleForegroundProperty = DependencyProperty.Register(
            "MainTitleForeground",
            typeof(Brush),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty MainTitleFontSizeProperty = DependencyProperty.Register(
            "MainTitleFontSize",
            typeof(double),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty MainTitleFontStretchProperty = DependencyProperty.Register(
            "MainTitleFontStretch",
            typeof(FontStretch),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MainTitleFontWeightProperty = DependencyProperty.Register(
            "MainTitleFontWeight",
            typeof(FontWeight),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty MainTitleFontStyleProperty = DependencyProperty.Register(
            "MainTitleFontStyle",
            typeof(FontStyle),
            typeof(GalleryView),
            new PropertyMetadata(null));


        public static readonly DependencyProperty MainTitleFontFamilyProperty = DependencyProperty.Register(
            "MainTitleFontFamily",
            typeof(FontFamily),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SubTitleStringFormatProperty = DependencyProperty.Register(
            "SubTitleStringFormat",
            typeof(string),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubTitleTemplateSelectorProperty = DependencyProperty.Register(
            "SubTitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubTitleTemplateProperty = DependencyProperty.Register(
            "SubTitleTemplate",
            typeof(DataTemplate),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(
            "SubTitle",
            typeof(object),
            typeof(GalleryView),
            new PropertyMetadata(null));
        public static readonly DependencyProperty MainTitleStringFormatProperty = DependencyProperty.Register(
            "MainTitleStringFormat",
            typeof(string),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MainTitleTemplateSelectorProperty = DependencyProperty.Register(
            "MainTitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MainTitleTemplateProperty = DependencyProperty.Register(
            "MainTitleTemplate",
            typeof(DataTemplate),
            typeof(GalleryView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MainTitleProperty = DependencyProperty.Register(
            "MainTitle",
            typeof(object),
            typeof(GalleryView),
            new PropertyMetadata(null));
    }
}
