using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IMorisaProjectInfo
    {
        string Name { get; set; }
        string FileName { get; set; }
        string Directory { get; set; }
    }
}
