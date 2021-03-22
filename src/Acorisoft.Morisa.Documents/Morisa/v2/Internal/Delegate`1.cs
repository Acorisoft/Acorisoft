using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Internal
{
    public static class Delegate<T>
    {
        public static readonly Predicate<T> AlmostTrue = x => true;
        public static readonly Predicate<T> AlmostFalse = x => false;
    }
}
