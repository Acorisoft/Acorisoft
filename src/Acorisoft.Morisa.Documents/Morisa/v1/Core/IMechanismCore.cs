using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    [Obsolete]
    public interface IMechanismCore
    {
        IObserver<ICompositionSet> Input { get; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
    [Obsolete]
    public interface IMechanismCore<T> : IMechanismCore
    {
        ReadOnlyObservableCollection<T> Collection { get; }
    }
}
