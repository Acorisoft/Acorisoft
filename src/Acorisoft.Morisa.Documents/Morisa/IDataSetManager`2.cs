using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IDataSetManager<TDataSet, TProfile> : IDataSetManager<TDataSet>
        where TDataSet : DataSet<TProfile>
        where TProfile : class, IProfile
    {
        public IObserver<TProfile> Profile { get; }
    }
}
