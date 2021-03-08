using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IMorisaProjectManager : ISubject<string , IMorisaProject>
    {

    }
}
