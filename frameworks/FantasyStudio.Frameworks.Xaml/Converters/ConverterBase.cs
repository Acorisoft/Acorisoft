using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Acorisoft.Frameworks.Converters
{
    public abstract class ConverterBase : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class OneWayConverter<TSource, TConvert> : ConverterBase
    {
        public override sealed object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TSource sourceValue)
            {
                return ConvertTo(sourceValue, parameter, culture);
            }

            return ConvertToFallback(value, parameter, culture) ?? value;
        }

        protected virtual TConvert ConvertTo(TSource rawValue, object parameter, CultureInfo culture)
        {
            return default(TConvert);
        }

        protected virtual object ConvertToFallback(object rawValue, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public abstract class TwoWayConverter<TSource, TConvert> : ConverterBase
    {
        public override sealed object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is TSource sourceValue)
            {
                return ConvertTo(sourceValue, parameter, culture);
            }

            return ConvertToFallback(value,parameter,culture) ?? value;
        }

        protected virtual TConvert ConvertTo(TSource rawValue,object parameter,CultureInfo culture)
        {
            return default(TConvert);
        }

        protected virtual object ConvertToFallback(object rawValue, object parameter, CultureInfo culture)
        {
            return null;
        }

        protected virtual object ConvertBackFallback(object rawValue, object parameter, CultureInfo culture)
        {
            return null;
        }

        protected virtual TSource ConvertBack(TConvert rawValue, object parameter, CultureInfo culture)
        {
            return default(TSource);
        }

        public override sealed object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TConvert convertValue)
            {
                return ConvertBack(convertValue, parameter, culture);
            }

            return ConvertBackFallback(value, parameter, culture) ?? value;
        }
    }
}
