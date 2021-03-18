using Acorisoft.Morisa.Assets;
using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Acorisoft.Morisa.Converters
{
    public class ResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Resource)
            {
                var bi = new BitmapImage();

                if(value is InDatabaseResource idr)
                {

                }
                else if(value is OutsideResource osr)
                {
                    bi.BeginInit();
                    bi.UriSource = new Uri(osr.FileName);
                    bi.EndInit();
                    return bi;
                }

            }

            return Images.DefaultCompositionSetCover;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
