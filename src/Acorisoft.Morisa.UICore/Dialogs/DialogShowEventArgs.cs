using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    internal class DialogShowEventArgs : EventArgs
    {
        public DialogShowEventArgs(IRoutableViewModel vm,DialogResult result, TaskCompletionSource<DialogResult> tcs)
        {
            ViewModel = vm ?? throw new ArgumentNullException(nameof(vm));
            Result = result;
            TCS = tcs;
        }

        internal TaskCompletionSource<DialogResult> TCS { get; }
        public IRoutableViewModel ViewModel { get; }
        public DialogResult Result { get; }
    }
}