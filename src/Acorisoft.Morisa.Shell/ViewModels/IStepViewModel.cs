using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.ViewModels
{
    public interface IStepViewModel : IViewModel
    {
        bool CanNext();
        bool CanLast();
        void Transfer(IViewModel context);
    }
}
