using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Colors
{
    public class MacaronColorGenerator : ColorGenerator
    {
        // H(%) S(31) B(91)
        public override Brush Generate()
        {
            var h = NumberGenerator.Next(0, 360);
            return FromHSB(h, .31d, .91d,
                (r, g, b) => new SolidColorBrush(Color.FromRgb((byte) r, (byte) g, (byte) b)));
        }
    }
}