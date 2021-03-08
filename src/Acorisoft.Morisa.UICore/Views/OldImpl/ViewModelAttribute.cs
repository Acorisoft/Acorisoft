using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Views
{
    [Obsolete]
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ViewModelAttribute : Attribute
    {


        // This is a positional argument
        public ViewModelAttribute(Type vm)
        {
            ViewModel = vm;
        }

        public Type ViewModel {
            get;
        }
    }
}
