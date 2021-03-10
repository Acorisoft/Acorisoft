using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IMorisaProjectTargetInfo
    {
        string Name { get; set; }
        string Summary { get; set; }
        string Topic { get; set; }
        string FileName { get; set; }
        string Directory { get; set; }
        ImageObject Cover { get; set; }
    }
}
