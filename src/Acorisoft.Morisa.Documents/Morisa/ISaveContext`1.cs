using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface ISaveContext<TProperty> where TProperty : IDataSetProperty
    {
        string FileName { get; set; }
        string Directory { get; set; }
        TProperty Property { get; }
    }
}
