using System.Globalization;
using System.Windows.Media;
using Acorisoft.Extensions.Platforms.Windows.Colors;

namespace Acorisoft.Extensions.Platforms.Windows.Converters
{
    public class BrushGeneratorConverter : OneWayConverter<string, Brush>
    {
        protected override Brush ConvertTo(string source, object parameter, CultureInfo cultureInfo)
        {
            return (Generator ?? new MorisaColorGenerator()).Generate();
        }

        protected override Brush FallbackValue(object source, object parameter, CultureInfo cultureInfo)
        {
            return (Generator ?? new MorisaColorGenerator()).Generate();
        }

        protected override Brush ConvertToFallbackValue(string source, object parameter, CultureInfo cultureInfo)
        {
            return (Generator ?? new MorisaColorGenerator()).Generate();
        }

        public ColorGenerator Generator { get; set; }
    }
}