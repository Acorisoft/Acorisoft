using System;
using System.Collections.Generic;
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


namespace Acorisoft.Morisa.Controls
{
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    public class DialogContentPresenter : ContentControl
    {
        static DialogContentPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogContentPresenter), new FrameworkPropertyMetadata(typeof(DialogContentPresenter)));
        }

        #region Title / Subtitle

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

        public object Subtitle
        {
            get => (object)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public DataTemplate SubtitleTemplate
        {
            get => (DataTemplate)GetValue(SubtitleTemplateProperty);
            set => SetValue(SubtitleTemplateProperty, value);
        }

        public DataTemplateSelector SubtitleTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(SubtitleTemplateSelectorProperty);
            set => SetValue(SubtitleTemplateSelectorProperty, value);
        }

        public string SubtitleStringFormat
        {
            get => (string)GetValue(SubtitleStringFormatProperty);
            set => SetValue(SubtitleStringFormatProperty, value);
        }

        #endregion Title / Subtitle

        #region Symbol

        public Brush SymbolColor
        {
            get => (Brush)GetValue(SymbolColorProperty);
            set => SetValue(SymbolColorProperty, value);
        }


        public double SymbolWidth
        {
            get => (double)GetValue(SymbolWidthProperty);
            set => SetValue(SymbolWidthProperty, value);
        }

        public double SymbolHeight
        {
            get => (double)GetValue(SymbolHeightProperty);
            set => SetValue(SymbolHeightProperty, value);
        }


        public object Symbol
        {
            get => (object)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        public DataTemplate SymbolTemplate
        {
            get => (DataTemplate)GetValue(SymbolTemplateProperty);
            set => SetValue(SymbolTemplateProperty, value);
        }

        public DataTemplateSelector SymbolTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(SymbolTemplateSelectorProperty);
            set => SetValue(SymbolTemplateSelectorProperty, value);
        }

        public string SymbolStringFormat
        {
            get => (string)GetValue(SymbolStringFormatProperty);
            set => SetValue(SymbolStringFormatProperty, value);
        }



        #endregion Symbol

        #region Buttons

        public object ButtonGroup
        {
            get => (object)GetValue(ButtonGroupProperty);
            set => SetValue(ButtonGroupProperty, value);
        }

        public DataTemplate ButtonGroupTemplate
        {
            get => (DataTemplate)GetValue(ButtonGroupTemplateProperty);
            set => SetValue(ButtonGroupTemplateProperty, value);
        }

        public DataTemplateSelector ButtonGroupTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ButtonGroupTemplateSelectorProperty);
            set => SetValue(ButtonGroupTemplateSelectorProperty, value);
        }

        public string ButtonGroupStringFormat
        {
            get => (string)GetValue(ButtonGroupStringFormatProperty);
            set => SetValue(ButtonGroupStringFormatProperty, value);
        }

        #endregion Buttons

        public static readonly DependencyProperty SymbolStringFormatProperty = DependencyProperty.Register(
            "SymbolStringFormat",
            typeof(string),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateSelectorProperty = DependencyProperty.Register(
            "SymbolTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolTemplateProperty = DependencyProperty.Register(
            "SymbolTemplate",
            typeof(DataTemplate),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonGroupStringFormatProperty = DependencyProperty.Register(
            "ButtonGroupStringFormat",
            typeof(string),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonGroupTemplateSelectorProperty = DependencyProperty.Register(
            "ButtonGroupTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonGroupTemplateProperty = DependencyProperty.Register(
            "ButtonGroupTemplate",
            typeof(DataTemplate),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonGroupProperty = DependencyProperty.Register(
            "ButtonGroup",
            typeof(object),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SymbolHeightProperty = DependencyProperty.Register(
            "SymbolHeight",
            typeof(double),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SymbolWidthProperty = DependencyProperty.Register(
            "SymbolWidth",
            typeof(double),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));


        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            "Symbol",
            typeof(object),
            typeof(DialogContentPresenter), 
            new PropertyMetadata(null));


        public static readonly DependencyProperty SymbolColorProperty = DependencyProperty.Register(
            "SymbolColor",
            typeof(Brush),
            typeof(DialogContentPresenter), 
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleStringFormatProperty = DependencyProperty.Register(
            "SubtitleStringFormat",
            typeof(string),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleTemplateSelectorProperty = DependencyProperty.Register(
            "SubtitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleTemplateProperty = DependencyProperty.Register(
            "SubtitleTemplate",
            typeof(DataTemplate),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register(
            "Subtitle",
            typeof(object),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));
        public static readonly DependencyProperty TitleStringFormatProperty = DependencyProperty.Register(
            "TitleStringFormat",
            typeof(string),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateSelectorProperty = DependencyProperty.Register(
            "TitleTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(DialogContentPresenter),
            new PropertyMetadata(null));

    }
}
