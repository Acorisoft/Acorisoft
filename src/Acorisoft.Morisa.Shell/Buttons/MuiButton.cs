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

namespace Acorisoft.Morisa.Buttons
{
    public class MuiButton : Button
    {
        private static readonly object DefaultCornerRadius = new CornerRadius(8);

        static MuiButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MuiButton), new FrameworkPropertyMetadata(typeof(MuiButton)));
        }


        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush HoverBackgroundBrush
        {
            get => (Brush)GetValue(HoverBackgroundBrushProperty);
            set => SetValue(HoverBackgroundBrushProperty, value);
        }

        public Brush HoverForegroundBrush
        {
            get => (Brush)GetValue(HoverForegroundBrushProperty);
            set => SetValue(HoverForegroundBrushProperty, value);
        }

        public Brush PressBackgroundBrush
        {
            get => (Brush)GetValue(PressBackgroundBrushProperty);
            set => SetValue(PressBackgroundBrushProperty, value);
        }

        public Brush PressForegroundBrush
        {
            get => (Brush)GetValue(PressForegroundBrushProperty);
            set => SetValue(PressForegroundBrushProperty, value);
        }

        public static readonly DependencyProperty PressForegroundBrushProperty = DependencyProperty.Register(
            "PressForegroundBrush",
            typeof(Brush),
            typeof(MuiButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty PressBackgroundBrushProperty = DependencyProperty.Register(
            "PressBackgroundBrush",
            typeof(Brush),
            typeof(MuiButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius",
            typeof(CornerRadius),
            typeof(MuiButton),
            new PropertyMetadata(DefaultCornerRadius));

        public static readonly DependencyProperty HoverForegroundBrushProperty = DependencyProperty.Register(
            "HoverForegroundBrush",
            typeof(Brush),
            typeof(MuiButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty HoverBackgroundBrushProperty = DependencyProperty.Register(
            "HoverBackgroundBrush",
            typeof(Brush),
            typeof(MuiButton),
            new PropertyMetadata(null));

    }

    public class SymbolButton : MuiButton
    {

        public Geometry Data
        {
            get => (Geometry)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data",
            typeof(Geometry),
            typeof(SymbolButton),
            new PropertyMetadata(null));
    }
    public class WindowCloseButton : MuiButton
    {
        static WindowCloseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowCloseButton), new FrameworkPropertyMetadata(typeof(WindowCloseButton)));
        }
    }
    public class WindowMinButton : MuiButton
    {
        static WindowMinButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowMinButton), new FrameworkPropertyMetadata(typeof(WindowMinButton)));
        }
    }
    

    public class WindowMaxButton : MuiButton
    {
        static WindowMaxButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowMaxButton), new FrameworkPropertyMetadata(typeof(WindowMaxButton)));
        }


        public WindowState WindowState
        {
            get => (WindowState)GetValue(WindowStateProperty);
            set => SetValue(WindowStateProperty, value);
        }

        public static readonly DependencyProperty WindowStateProperty = DependencyProperty.Register(
            "WindowState",
            typeof(WindowState),
            typeof(WindowMaxButton),
            new PropertyMetadata((object)WindowState.Normal));
    }

    public class WindowGoBackButton : MuiButton
    {
        static WindowGoBackButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowGoBackButton), new FrameworkPropertyMetadata(typeof(WindowGoBackButton)));
        }
    }
}
