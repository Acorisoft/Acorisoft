using Acorisoft.Morisa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public abstract class DialogFunction : ViewModelBase, IResultable
    {
        protected virtual bool VerifyModelCore()
        {
            return true;
        }

        protected virtual object GetResultCore()
        {
            return this;
        }

        object IResultable.GetResult()
        {
            return GetResultCore();
        }

        bool IResultable.VerifyModel()
        {
            return VerifyModelCore();
        }
    }
}
