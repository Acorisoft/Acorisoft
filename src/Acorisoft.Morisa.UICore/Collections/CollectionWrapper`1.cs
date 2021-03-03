using DynamicData.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace Acorisoft.Morisa.Collections
{
    public class CollectionWrapper<TElement, TList> : IReadOnlyCollection<TElement>, INotifyCollectionChanged, INotifyPropertyChanged where TList : IList<TElement>
    {
        public CollectionWrapper(TList list)
        {
        }
        public int Count => throw new NotImplementedException();

        public IEnumerator<TElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
