using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Persistants
{
    public class CompositionSetChangedEventArgs : EventArgs
    {
        public CompositionSetChangedEventArgs(ICompositionSet oldCs, ICompositionSet newCs)
        {
            OldValue = oldCs;
            NewValue = newCs ?? throw new ArgumentNullException(nameof(newCs));
        }

        public ICompositionSet OldValue { get; }
        public ICompositionSet NewValue { get; }
    }

    public class CompositionSetOpenedEventArgs : EventArgs
    {
        public CompositionSetOpenedEventArgs(ICompositionSetStore store)
        {
            NewValue = store ?? throw new ArgumentNullException(nameof(store));
        }
        public ICompositionSetStore NewValue { get; }
    }

    public interface ICompositionSetManager
    {
        void Load(string target);
        void Load(ICompositionSetInfo info);
        void Load(ICompositionSetStore store);
        event EventHandler<CompositionSetOpenedEventArgs> Opened;
        event EventHandler<CompositionSetChangedEventArgs> Changed;
    }
}
