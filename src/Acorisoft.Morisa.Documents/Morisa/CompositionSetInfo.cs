using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class CompositionSetInfo : ICompositionSetInfo
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Topic { get; set; }
        public List<string> Tags { get; set; }
        public InDatabaseResource Cover { get; set; }
    }
}
