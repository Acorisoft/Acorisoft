using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IMapGroupAdapter : IMapGroup
    {
        ICollection<IMapGroup> Children { get; } 
        ICollection<IMapBrush> Brushes { get; }
        IMapGroup Source { get; }
    }
}
