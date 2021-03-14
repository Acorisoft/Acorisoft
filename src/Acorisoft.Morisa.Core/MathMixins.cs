using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public static class MathMixins
    {
        public static double MinMax(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
