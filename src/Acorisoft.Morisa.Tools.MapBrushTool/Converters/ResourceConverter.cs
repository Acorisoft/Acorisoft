using Acorisoft.Morisa.Map;
using Splat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Acorisoft.Morisa.Tools.Converters
{
    public sealed class ResourceConverter : IValueConverter
    {
        private static Lazy<IBrushSetFactory> FactoryImpl => new Lazy<IBrushSetFactory>(() => Locator.Current.GetService<IBrushSetFactory>());

        public static IBrushSetFactory Factory => FactoryImpl.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is InDatabaseResource idr)
            {
                using (var ms = new MemoryStream())
                {
                    using (var stream = Factory.GetResource(idr))
                    {
                        stream.CopyTo(ms);
                        var bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                        return bi;
                    }
                }
            }
            else if (value is IBrushAdapter brush)
            {
                using (var stream = Factory.GetResource(brush))
                {
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    ms.Position = 0;
                    var bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    return bi;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
