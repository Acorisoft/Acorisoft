using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Acorisoft.Extensions.Platforms.Windows.Converters;
using Acorisoft.Extensions.Platforms.Windows.Services;

namespace Acorisoft.Studio.Converters
{
    public class CompositionSetImageConverter : OneWayConverter<Uri,ImageSource>
    {
        protected override ImageSource ConvertTo(Uri source, object parameter, CultureInfo cultureInfo)
        {
            var stream = ServiceLocator.FileManagerService.GetStream(source);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            return bi;
        }

        protected override ImageSource FallbackValue(object source, object parameter, CultureInfo cultureInfo)
        {
            return base.FallbackValue(source, parameter, cultureInfo);
        }

        protected override ImageSource ConvertToFallbackValue(Uri source, object parameter, CultureInfo cultureInfo)
        {
            //
            // 当
            return base.ConvertToFallbackValue(source, parameter, cultureInfo);
        }
    }
}