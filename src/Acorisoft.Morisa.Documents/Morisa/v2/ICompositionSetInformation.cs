using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2
{
    public interface ICompositionSetInformation : IProfile
    {
        string Topic { get; set; }        
    }
}
