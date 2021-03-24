using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public interface IMapData : IImmutable
    {
        double X { get; }
        double Y { get; }
    }
}
