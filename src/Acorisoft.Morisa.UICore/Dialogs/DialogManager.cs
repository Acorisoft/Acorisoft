using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public class DialogManager : IDialogService
    {
        private readonly RoutingState _router;

        public DialogManager()
        {
            _router = new RoutingState();
        }

        public Task<DialogResult> Dialog<T>() where T : IRoutableViewModel
        {
            var vm = Locator.Current.GetService<T>();
            var result = new DialogResult();
            var tcs = new TaskCompletionSource<DialogResult>();
            var e = new DialogShowEventArgs(vm,result,tcs);
            DemandDialogShow?.Invoke(this, e);
            return tcs.Task;
        }
        public RoutingState Router => _router;
        internal event EventHandler<DialogShowEventArgs> DemandDialogShow;
    }
}
