using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Routers
{
    public class NavigationFilterEventArgs : EventArgs
    {
        private IRoutableViewModel _result;

        public NavigationFilterEventArgs(IRoutableViewModel oldVM, IRoutableViewModel newVM)
        {
            Old = oldVM;
            New = newVM;
        }

        public IFullLogger Logger { get; internal set; }
        public IRoutableViewModel Old { get; }
        public IRoutableViewModel New { get; }
        public bool CanNavigate { get; set; }
        public IRoutableViewModel Result
        {
            get
            {
                if (CanNavigate)
                {
                    return New;
                }
                else
                {
                    return _result;
                }
            }
            set
            {
                if(CanNavigate)
                {
                    _result = New;
                }
                else{
                    _result = value;
                }
            }
        }
    }
}
