using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IMechanismCore
    {
        IObserver<ICompositionSet> Input { get; }
    }

    public interface IMechanismCore<T> : IMechanismCore
    {
        ReadOnlyObservableCollection<T> Collection { get; }
    }
}
