using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSet
    {
        protected internal LiteDatabase Database { get; set; }
    }
}
