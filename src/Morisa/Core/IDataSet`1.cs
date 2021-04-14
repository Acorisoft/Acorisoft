using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataSet<TProperty> : IDataSet where TProperty : IDataProperty
    {
        TProperty Property { get; set; }
    }
}
