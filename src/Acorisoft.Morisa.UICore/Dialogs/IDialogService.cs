using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public interface IDialogService
    {
        Task<DialogSession> Dialog<T>() where T : IRoutableViewModel;
        Task<DialogSession> Dialog(Type dialogVM);
        Task<DialogSession> Notification();
        Task<DialogSession> Notification(Notification notification);
        Task<DialogSession> MessageBox(string title, string content, string subTitle = "");
    }
}
