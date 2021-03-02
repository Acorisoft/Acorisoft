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
        Task<DialogResult> Dialog<T>() where T : IRoutableViewModel;
    }
}
