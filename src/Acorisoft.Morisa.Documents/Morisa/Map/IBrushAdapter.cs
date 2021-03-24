using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IBrushAdapter : IBrush
    {
        bool IsSelected { get; set; }
        DateTime Creation { get; }
        IBrush Source { get; }
    }
}
