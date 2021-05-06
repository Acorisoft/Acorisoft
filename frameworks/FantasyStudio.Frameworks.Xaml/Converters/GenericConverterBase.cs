using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Acorisoft.Frameworks.Converters
{
    public abstract class GenericConverterBase<TFrom, TBack> : ConverterBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override sealed object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertFromCore(value, targetType, parameter, culture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override sealed object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBackCore(value, targetType, parameter, culture);
        }

        protected object ConvertFromCore(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TFrom genericValue)
            {
                return ConvertFromCore(genericValue, parameter, culture);
            }
            return ConvertFromFallbackValue(value, targetType, parameter, culture);
        }
        protected object ConvertBackCore(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TBack genericValue)
            {
                return ConvertBackCore(genericValue, parameter, culture);
            }
            return ConvertBackFallbackValue(value, targetType, parameter, culture);
        }
        protected abstract object ConvertFromCore(TFrom value, object parameter, CultureInfo culture);
        protected abstract object ConvertBackCore(TBack value, object parameter, CultureInfo culture);
        protected abstract object ConvertFromFallbackValue(object value, Type targetType, object parameter, CultureInfo culture);
        protected abstract object ConvertBackFallbackValue(object value, Type targetType, object parameter, CultureInfo culture);
    }
}