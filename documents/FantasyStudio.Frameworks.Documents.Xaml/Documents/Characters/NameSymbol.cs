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

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    /// <summary>
    /// 
    /// </summary>
    public class NameSymbol : Control
    {
        static NameSymbol()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameSymbol), new FrameworkPropertyMetadata(typeof(NameSymbol)));
        }

        public Gender Gender
        {
            get => (Gender)GetValue(GenderProperty);
            set => SetValue(GenderProperty, value);
        }

        public static readonly DependencyProperty GenderProperty = DependencyProperty.Register(
            "Gender",
            typeof(Gender),
            typeof(NameSymbol), 
            new PropertyMetadata(null));

    }

    public class GenderSymbol : Control
    {
        static GenderSymbol()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GenderSymbol), new FrameworkPropertyMetadata(typeof(GenderSymbol)));
        }

        public Gender Gender
        {
            get => (Gender)GetValue(GenderProperty);
            set => SetValue(GenderProperty, value);
        }

        public static readonly DependencyProperty GenderProperty = DependencyProperty.Register(
            "Gender",
            typeof(Gender),
            typeof(GenderSymbol),
            new PropertyMetadata(null));

    }
}
