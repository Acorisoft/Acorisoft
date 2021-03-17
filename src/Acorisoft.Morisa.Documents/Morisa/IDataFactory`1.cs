using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using DryIoc;
using DynamicData;

namespace Acorisoft.Morisa
{
    public interface IDataFactorySelectable<TElement>
    {
        void ChangeSelector(Func<TElement, bool> selector);
        void ChangeSelector(IComparer<TElement> selector);
        void ChangeSelector(IPageRequest selector);
    }

    public interface IDataFactory<TElement> : IDataFactorySelectable<TElement>
    {
        void Add(TElement element);
        void AddRange(IEnumerable<TElement> elements);
        bool Remove(TElement element);
        void RemoveRange(int index, int count);
        void Clear();

        IObserver<Func<TElement, bool>> Filter { get; }
        IObserver<IComparer<TElement>> Sorter { get; }
        ReadOnlyObservableCollection<TElement> Collection { get; }
    }
}