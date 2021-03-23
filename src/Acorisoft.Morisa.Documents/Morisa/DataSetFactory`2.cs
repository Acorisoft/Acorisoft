using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSetFactory<TDataSet, TProperty> : DataSetFactory<TDataSet>, IDataSetFactory<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
    }
}
