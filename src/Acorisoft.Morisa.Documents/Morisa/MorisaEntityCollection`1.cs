using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class MorisaEntityCollection<TElement, TList> : INotifyCollectionChanged, INotifyPropertyChanged where TList : class, INotifyCollectionChanged, IList<TElement>, new()
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TList _AssociatedCollection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangedEventHandler ChangedHandler;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected NotifyCollectionChangedEventHandler CollectionHandler;


        public MorisaEntityCollection(TList collection)
        {
            _AssociatedCollection = collection ?? new TList();
        }

        protected bool SetValueAndRaiseUpdate<T>(ref T backendField, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backendField, value))
            {
                backendField = value;
                ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
                return true;
            }

            return false;
        }

        protected void RaiseUpdate([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add => CollectionHandler += value;
            remove => CollectionHandler -= value;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => ChangedHandler += value;
            remove => ChangedHandler -= value;
        }

        public TList Collection
        {
            get => _AssociatedCollection;
        }

        public IObservable<NotifyCollectionChangedEventArgs> ObserveCollectionChanged { get; }
    }
}
