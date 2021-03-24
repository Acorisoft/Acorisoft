using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSetProperty : IDataSetProperty
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Authors { get; set; }
        public Resource Cover { get; set; }
    }
}
