using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{

    interface IDataSetImpl
    {
        LiteDatabase Database { get; }
    }


    public interface IDataSet : IDisposable
    {
    }
}
