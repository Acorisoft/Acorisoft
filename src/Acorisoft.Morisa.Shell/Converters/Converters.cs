using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Acorisoft.Morisa.Converters
{
    public static class Converters
    {
        public static IValueConverter Double2CornerRadius { get; } = new Double2CornerRadiusConverter();
    }
}
