using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class SaveContext<TProperty> : ISaveContext<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
        public SaveContext(TProperty property)
        {
            Property = property;
        }

        public string FileName { get; set; }
        public string Directory { get; set; }

        public TProperty Property { get; }
    }
}
