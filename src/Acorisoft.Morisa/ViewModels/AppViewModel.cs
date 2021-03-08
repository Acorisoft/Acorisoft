using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Acorisoft.Morisa.ViewModels
{
#pragma warning disable IDE0060

    public class AppViewModel : ViewModelBase
    {
        private IMorisaProject _project;
        private readonly IMorisaProjectManager _projMgr;

        public AppViewModel(IMorisaProjectManager projMgr)
        {
            _projMgr.ObserveOn(RxApp.MainThreadScheduler).Subscribe(OnProjectChanged);
            this.WhenAnyValue(x => x.Project).ObserveOn()
                
        }

        protected void OnProjectChanged(IMorisaProject newProject)
        {
            if (!ReferenceEquals(_project , newProject))
            {

            }
        }

        public IMorisaProject Project { get; set; }
    }
}
