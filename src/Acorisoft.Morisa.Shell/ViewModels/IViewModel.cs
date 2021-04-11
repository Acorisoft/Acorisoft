using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.ViewModels
{
    public interface IViewModel : IRoutableViewModel, IParameterTransfer
    {
        string Title { get; }
    }
}
