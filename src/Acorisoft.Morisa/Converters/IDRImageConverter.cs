using Acorisoft.Morisa.Assets;
using Acorisoft.Morisa.Core;
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

namespace Acorisoft.Morisa.Converters
{
    public class IDRImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is InDatabaseResource idr)
            {
                var bi = new BitmapImage();
                var cs = ViewModelLocator.AppViewModel.CurrentProject;
                
                if(cs is CompositionSet set)
                {
                    
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
