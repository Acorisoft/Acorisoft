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
    }
}
