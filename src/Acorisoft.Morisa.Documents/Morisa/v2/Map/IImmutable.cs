using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public interface IImmutable
    {
        IImmutable AsMutable(double x, double y);
        IImmutable AsMutable(Brush brush);
    }
}
