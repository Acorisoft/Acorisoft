using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataSetFactory{TDataSet, TProperty}"/> 类型表示
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class DataSetFactory<TDataSet, TProperty> : DataSetFactory<TDataSet>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {

    }
}
