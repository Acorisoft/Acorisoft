using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    interface IStepViewModelContext
    {
        void GetContext(object context);
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

        protected virtual void GetContextCore(object context)
        {
            if(context is T targetContext)
            {
                Context = targetContext;
            }
        }

        void IStepViewModelContext.GetContext(object context)
        {
            GetContextCore(context);
        }

        protected T Context { get; private set; }
    }
}
