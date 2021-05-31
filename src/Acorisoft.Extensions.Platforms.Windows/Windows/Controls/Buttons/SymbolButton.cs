using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Buttons
{
    public class SymbolButton : Button
    {
        
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register("Symbol", typeof(Geometry), typeof(SymbolButton), new PropertyMetadata(default(Geometry)));
        public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight", typeof(Brush), typeof(SymbolButton), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty SymbolWidthProperty = DependencyProperty.Register(
            "SymbolWidth", typeof(double), typeof(SymbolButton), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty SymbolHeightProperty = DependencyProperty.Register(
            "SymbolHeight", typeof(double), typeof(SymbolButton), new PropertyMetadata(default(double)));

        public double SymbolHeight
        {
            get => (double) GetValue(SymbolHeightProperty);
            set => SetValue(SymbolHeightProperty, value);
        }
        public double SymbolWidth
        {
            get => (double) GetValue(SymbolWidthProperty);
            set => SetValue(SymbolWidthProperty, value);
        }
        public Brush Highlight
        {
            get => (Brush) GetValue(HighlightProperty);
            set => SetValue(HighlightProperty, value);
        }
        public Geometry Symbol
        {
            get => (Geometry) GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }
        
      
    }
}