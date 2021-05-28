using System;
using System.Globalization;
using System.Windows.Data;

namespace Acorisoft.Extensions.Platforms.Windows.Converters
{
    public class OneWayConverter<TSource, TConvertTo> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return ConvertToFallbackValue(default, parameter, culture);
            }

            if (value is TSource source)
            {
                return ConvertTo(source, parameter, culture);
            }

            return FallbackValue(value, parameter, culture);
        }

        protected virtual TConvertTo FallbackValue(object source, object parameter, CultureInfo cultureInfo)
        {
            return default;
        }
        
        protected virtual TConvertTo ConvertToFallbackValue(TSource source, object parameter, CultureInfo cultureInfo)
        {
            return default;
        }
        
        protected virtual  TConvertTo ConvertTo(TSource source, object parameter, CultureInfo cultureInfo)
        {
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}