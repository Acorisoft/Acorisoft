using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    internal interface ICompositionSetDatabase
    {
        LiteDatabase MainDatabase { get; }
    }
    public interface ICompositionSet : IDisposable
    {
    }
}
