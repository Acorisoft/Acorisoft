using System.Globalization;
using Acorisoft.Extensions.Platforms.Windows.Converters;
using Acorisoft.Studio.ProjectSystem;

namespace Acorisoft.Studio.Converters
{
    public class CompositionSetNameConverter : OneWayConverter<ICompositionSet,string>
    {
        protected override string ConvertTo(ICompositionSet source, object parameter, CultureInfo cultureInfo)
        {
            return source?.Name;
        }

        protected override string FallbackValue(object source, object parameter, CultureInfo cultureInfo)
        {
            return "未加载项目";
        }

        protected override string ConvertToFallbackValue(ICompositionSet source, object parameter, CultureInfo cultureInfo)
        {
            return "未加载项目";
        }
    }
}