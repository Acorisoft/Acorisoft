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

        public Task<DialogSession> Dialog<T>() where T : IRoutableViewModel
        {
            var vm = Locator.Current.GetService<T>();
            var e = new DialogShowEventArgs(
                vm, 
                GetDialogSession(),
                new TaskCompletionSource<DialogSession>());
            DemandDialogShow?.Invoke(this, e);
            return e.TCS.Task;
        }

        public DialogSession GetDialogSession() => new DialogSession(this);

        public RoutingState Router => _router;
        internal event EventHandler<DialogShowEventArgs> DemandDialogShow;
    }
}
