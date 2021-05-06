using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Frameworks.Converters
{
    public class String2DateTimeConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy/MM/dd");
            }
            return DateTime.Now.ToString("yyyy/MM/dd");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out var datetime))
            {
                return datetime;
            }

            return DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
    
    public class String2IntConverter: ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int number)
            {
                return number.ToString();
            }
            return "0";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value?.ToString() ?? string.Empty;
            var newStr = new string(str.Where(x => char.IsDigit(x)).ToArray());
            if (int.TryParse(newStr, out var number))
            {
                return number;
            }

            return 0d;
        }
    }
}
