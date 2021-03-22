using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSet<TProfile> : DataSet where TProfile : class
    {
        protected internal TProfile Setting { get; internal set; }
    }
}
