using System.Globalization;
using Acorisoft.Extensions.Platforms.Windows.Converters;

namespace Acorisoft.Studio.Converters
{
    public class GalleryTitleConverter : OneWayConverter<int,string>
    {
        protected override string ConvertTo(int source, object parameter, CultureInfo cultureInfo)
        {
            return $"当前存在{source}个项目";
        }

        protected override string FallbackValue(object source, object parameter, CultureInfo cultureInfo)
        {
            return "不存在项目";
        }

        protected override string ConvertToFallbackValue(int source, object parameter, CultureInfo cultureInfo)
        {
            return "不存在项目";
        }
    }
}