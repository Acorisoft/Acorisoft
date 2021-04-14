using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public abstract class DataSet<TProperty> : DataSet
        where TProperty : DataProperty, IDataProperty
    {
        public TProperty Property { get; set; }
    }
}
