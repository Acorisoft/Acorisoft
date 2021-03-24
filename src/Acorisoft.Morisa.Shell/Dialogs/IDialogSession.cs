using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public interface IUpdatableSession : IDialogSession
    {
        Task<IDialogSession> Update<TViewModel>() where TViewModel : IRoutableViewModel;
    }

    public interface IDialogSession
    {
        bool IsCompleted { get; }
        IRoutableViewModel ViewModel { get; }

        public T GetResult<T>() where T : class;
    }
}
