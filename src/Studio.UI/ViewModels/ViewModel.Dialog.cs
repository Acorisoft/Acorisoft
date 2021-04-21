using Acorisoft.Spa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ViewModels
{
    public abstract class DialogViewModel : ViewModelBase, IDialogViewModel
    {
        public virtual object GetResult()
        {
            return this;
        }
    }
}
