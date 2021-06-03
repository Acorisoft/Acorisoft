using System.Windows.Media;

namespace Acorisoft.Extensions.Platforms.Windows.Colors
{
    public class MorisaColorGenerator : ColorGenerator
    {
        public override Brush Generate()
        {
            var h = NumberGenerator.Next(0, 360);
            return FromHSB(h, .62d, .76d,
                (r, g, b) => new SolidColorBrush(Color.FromRgb((byte) r, (byte) g, (byte) b)));
        }
    }
}