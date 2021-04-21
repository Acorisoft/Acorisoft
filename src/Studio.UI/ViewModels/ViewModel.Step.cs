using Acorisoft.Spa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ViewModels
{
    public abstract class StepViewModel : ViewModelBase, IStepViewModel
    {
        public virtual bool CanIgnore() => true;
        public virtual bool CanLast() => true;
        public virtual bool CanNext() => true;
        
        void IStepViewModel.Context(object context)
        {
            Context = context;
        }

        protected object Context { get; private set; }

    }
}
