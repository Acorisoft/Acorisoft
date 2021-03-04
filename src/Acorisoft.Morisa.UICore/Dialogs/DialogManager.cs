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

        public Task<DialogSession> Dialog(Type dialogVM)
        {
            if (dialogVM.IsAssignableTo(dialogVM))
            {
                var vm = Locator.Current.GetService(dialogVM) as IRoutableViewModel;
                var e = new DialogShowEventArgs(
                vm,
                GetDialogSession(),
                new TaskCompletionSource<DialogSession>());
                DemandDialogShow?.Invoke(this, e);
                return e.TCS.Task;
            }

            return null;
        }

        public Task<DialogSession> Notification()
        {
            return Notification(new Notification());
        }

        public Task<DialogSession> Notification(Notification notification)
        {
            var e = new DialogShowEventArgs(
                notification,
                GetDialogSession(),
                new TaskCompletionSource<DialogSession>());
            DemandDialogShow?.Invoke(this, e);
            return e.TCS.Task;
        }

        public Task<DialogSession> MessageBox(string title,string content,string subTitle = "")
        {
            var e = new DialogShowEventArgs(
                new MessageBox
                {
                    Subtitle = subTitle,
                    Title = title,
                    Content = content
                },
                GetDialogSession(),
                new TaskCompletionSource<DialogSession>());
            DemandDialogShow?.Invoke(this, e);
            return e.TCS.Task;
        }

        public RoutingState Router => _router;
        internal event EventHandler<DialogShowEventArgs> DemandDialogShow;
    }
}
