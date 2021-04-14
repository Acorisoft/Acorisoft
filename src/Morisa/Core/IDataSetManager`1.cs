using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataSetManager<TDataSet> : IDisposable where TDataSet : DataSet, IDataSet
    {
    }
}
