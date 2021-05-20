using System.Collections;
using System.Collections.Generic;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows.Dialogs
{
    public interface IDialogContext
    {
        IViewModel Share { get; }
        IEnumerable<IDialogViewModel> ViewModels { get; }
    }
}