using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IMapLayer
    {
        bool HitTest(int x, int y);
        bool HitTest(double x, double y);
    }
}
