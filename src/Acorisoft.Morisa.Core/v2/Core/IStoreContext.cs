using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IStoreContext
    {
        string Name { get; set; }
        string FileName { get; set; }
        string Directory { get; set; }
    }
}
