using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IMapData : IImmutable
    {
        double X { get; }
        double Y { get; }
    }
}
