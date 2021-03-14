using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public interface IDialogManager
    {
        Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IRoutableViewModel;
        Task<bool> MessageBox(string title, string content);
    }
}
