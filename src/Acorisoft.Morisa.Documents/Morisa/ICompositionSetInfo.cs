using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface ICompositionSetInfo
    {
        string Directory { get; set; }
        string FileName { get; set; }
        string Name { get; set; }
        string Summary { get; set; }
        string Topic { get; set; }
        List<string> Tags { get; set; }
        InDatabaseResource Cover { get; set; }
    }
}
