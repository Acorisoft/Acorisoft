using System;
using System.Globalization;
using System.Windows.Data;

namespace Acorisoft.Extensions.Platforms.Windows.Converters
{
    public abstract class ConverterBase<TSource, TTarget> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //
            // 从原始属性转化到目标属性的过程。
            if (value is null)
            {
                //
                // 处理Null状态
                return ConvertSourceToTargetWhenNull(parameter, culture);
            }

            if (value is TSource source)
            {
                return ConvertSourceToTarget(source, parameter, culture);
            }

            return ConvertSourceToTargetFallback(value, parameter, culture);
        }

        protected virtual object ConvertSourceToTarget(TSource source, object parameter, CultureInfo culture) => default(TTarget);
        protected virtual object ConvertSourceToTargetFallback(object source, object parameter, CultureInfo culture) => default(TTarget);
        protected virtual object ConvertSourceToTargetWhenNull(object parameter, CultureInfo culture) => default(TTarget);
        protected virtual object ConvertTargetToSourceWhenNull(object parameter, CultureInfo culture) => default(TSource);
        protected virtual object ConvertTargetToSource(TTarget target, object parameter, CultureInfo culture) => default(TSource);
        protected virtual object ConvertTargetToSourceFallback(object target, object parameter, CultureInfo culture) => default(TSource);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //
            // 从目标属性转化到原始属性的过程。
            //
            // 从原始属性转化到目标属性的过程。
            if (value is null)
            {
                //
                // 处理Null状态
                return ConvertTargetToSourceWhenNull(parameter, culture);
            }

            if (value is TTarget target)
            {
                return ConvertTargetToSource(target, parameter, culture);
            }

            return ConvertTargetToSourceFallback(value, parameter, culture);
        }
    }
}