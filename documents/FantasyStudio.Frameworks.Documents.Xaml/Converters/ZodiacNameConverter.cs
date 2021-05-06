using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.FantasyStudio.Documents.Characters;
using Acorisoft.Frameworks.Converters;

namespace Acorisoft.FantasyStudio.Converters
{
    public class ZodiacNameConverter : OneWayConverter<Zodiac, string>
    {
        protected override string ConvertTo(Zodiac rawValue, object parameter, CultureInfo culture)
        {
            return CharacterZodiacDefinition.GetName(rawValue, culture);
        }

        protected override object ConvertToFallback(object rawValue, object parameter, CultureInfo culture)
        {
            return culture.LCID switch
            {
                2052 => "白羊座",
                1028 or 3076 => "白羊座",
                _ => "Aries",
            };
        }
    }
}
