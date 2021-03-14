using Acorisoft.Morisa.Assets;
using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Acorisoft.Morisa.Converters
{
    public class IDROfflineImageConverter : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            if(value is InDatabaseResource idr)
            {
                if (File.Exists(idr.FileName))
                {
                    return new BitmapImage(new Uri(idr.FileName));
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
