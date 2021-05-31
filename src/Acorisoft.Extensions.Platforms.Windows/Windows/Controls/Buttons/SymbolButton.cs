using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Buttons
{
    public class SymbolButton : Button
    {
        
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register("Symbol", typeof(Geometry), typeof(SymbolButton), new PropertyMetadata(default(Geometry)));
        public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight", typeof(Brush), typeof(SymbolButton), new PropertyMetadata(default(Brush)));

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