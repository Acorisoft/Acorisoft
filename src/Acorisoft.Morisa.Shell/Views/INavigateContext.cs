using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    public interface INavigateContext
    {
        IRoutableViewModel NavigateIntend { get; }
        IRoutableViewModel NavigateFrom { get; }
        IRoutableViewModel NavigateTo { get; }
        INavigateParameter NavigateFromParameters { get; }
        INavigateParameter NavigateToParameters { get; }
    }
}
