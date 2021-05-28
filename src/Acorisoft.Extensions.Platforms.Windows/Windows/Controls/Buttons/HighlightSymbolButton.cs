using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Controls.Buttons
{
    public class HighlightSymbolButton : Button
    {
        
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register("Symbol", typeof(Geometry), typeof(HighlightSymbolButton), new PropertyMetadata(default(Geometry)));
        public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight", typeof(Brush), typeof(HighlightSymbolButton), new PropertyMetadata(default(Brush)));

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