using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface ICompositionSetContext
    {
        string Directory { get; }
        string FileName { get; }
        ICompositionSet CompositionSet { get; }
    }
}
