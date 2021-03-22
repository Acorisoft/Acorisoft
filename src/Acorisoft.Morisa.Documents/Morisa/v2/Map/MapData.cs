using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public abstract class MapData : IMapData
    {


        public IImmutable AsMutable(double x, double y)
        {
            return new TerrainData(x, y, Brush);
        }

        public IImmutable AsMutable(Brush brush)
        {
            return new TerrainData(X, Y, brush);
        }

        public Brush Brush { get; protected set; }
        public double X { get; protected set; }
        public double Y { get; protected set; }
    }
}
