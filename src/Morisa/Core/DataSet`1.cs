using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="DataSet{TProperty}"/>
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class DataSet<TProperty> : DataSet
        where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 
        /// </summary>
        public TProperty Property { get; set; }
    }
}
