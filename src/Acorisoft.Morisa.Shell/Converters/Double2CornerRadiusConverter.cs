using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Acorisoft.Morisa.Converters
{
    public class Double2CornerRadiusConverter : IValueConverter
    {
        public static readonly CornerRadius Zero = new CornerRadius(0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double valD)
            {
                return new CornerRadius(valD);
            }
            else if(value is float valF)
            {
                return new CornerRadius(valF);
            }
            else if(value is long valL)
            {
                return new CornerRadius(valL);
            }
            else if(value is int valI)
            {
                return new CornerRadius(valI);
            }
            else if(value is short valS)
            {
                return new CornerRadius(valS);
            }
            else if(value is byte valB)
            {
                return new CornerRadius(valB);
            }
            else if(value is uint valUI)
            {
                return new CornerRadius(valUI);
            }
            else if(value is ulong valUL)
            {
                return new CornerRadius(valUL);
            }
            else if(value is ushort valUS)
            {
                return new CornerRadius(valUS);
            }
            else if (value is sbyte valSB)
            {
                return new CornerRadius(valSB);
            }
            return Zero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
