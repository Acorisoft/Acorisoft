using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2
{
    public interface IProfile
    {
        string Name { get; set; }
        string Summary { get; set; }
        string Authors { get; set; }
        Resource Cover { get; set; }
    }
}
