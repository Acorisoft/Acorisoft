using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public class DialogSession
    {
        internal DialogSession(IDialogService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public T GetResult<T>() where T : class, IRoutableViewModel
        {
            return Result as T;
        }

        public Task<DialogSession> NewSession<T>() where T : IRoutableViewModel
        {
            return Service.Dialog<T>();
        }

        internal IDialogService Service { get; private set; }

        public IRoutableViewModel Result { get; set; }
        public bool IsCompleted { get; internal set; }
    }
}
