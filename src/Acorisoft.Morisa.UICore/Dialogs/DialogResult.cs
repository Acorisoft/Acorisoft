using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public class DialogResult
    {
        public T GetResult<T>() where T : class, IRoutableViewModel
        {
            return Result as T;
        }

        public IRoutableViewModel Result { get; set; }
        public bool IsCompleted { get; internal set; }
    }
}
