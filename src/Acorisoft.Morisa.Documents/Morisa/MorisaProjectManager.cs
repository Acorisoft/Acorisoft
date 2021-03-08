using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class MorisaProjectManager : IMorisaProjectManager
    {
        public IObservable<IMorisaProjectInfo> ProjectInfo => throw new NotImplementedException();

        public IObservable<IMorisaProject> Project => throw new NotImplementedException();
    }
}
