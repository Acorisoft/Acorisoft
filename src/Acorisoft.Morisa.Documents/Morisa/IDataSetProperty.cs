using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IDataSetProperty
    {
        string Name { get; set; }
        string Summary { get; set; }
        string Authors { get; set; }
        Resource Cover { get; set; }
    }
}
