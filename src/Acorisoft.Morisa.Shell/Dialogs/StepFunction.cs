using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    interface IStepViewModelContext
    {
        void PostContext(object context);
    }

    public abstract class StepFunction<T> : DialogFunction, IStepViewModelContext
    {
        
        protected virtual object GetContextCore()
        {
            return this;
        }

        protected override object GetResultCore()
        {
            return Context;
        }

        protected virtual void OnGetContext(object context)
        {
            if(context is T targetContext)
            {
                Context = targetContext;
            }
        }

        void IStepViewModelContext.PostContext(object context)
        {
            OnGetContext(context);
        }

        protected T Context { get; private set; }
    }
}
