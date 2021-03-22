using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IDataSetFactory<TDataSet,TInformation> : IDataSetFactory<TDataSet>
        where TDataSet : class , IDataSet<TInformation>
        where TInformation : class , IInformation
    {
    }
}
