using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{

    public abstract class DataSetManager<TDataSet> : Disposable, IDataSetManager<TDataSet> where TDataSet : DataSet, IDataSet
    {
    }
}
