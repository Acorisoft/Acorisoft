using System.Globalization;
using Acorisoft.Extensions.Platforms.Windows.Converters;
using Acorisoft.Studio.Core;
using Acorisoft.Studio.Properties;

namespace Acorisoft.Studio.Converters
{
    public class ComposeSetNameConverter : OneWayConverter<IComposeSet, string>
    {
        protected override string ConvertTo(IComposeSet source, object parameter, CultureInfo cultureInfo)
        {
            return source.Property.Name;
        }

    }
}