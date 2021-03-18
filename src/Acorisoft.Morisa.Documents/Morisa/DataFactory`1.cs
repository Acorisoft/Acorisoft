using DynamicData;
using DynamicData.Binding;
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

namespace Acorisoft.Morisa
{
    public abstract class DataFactory<TElement> : DataFactory, IDataFactory<TElement>
    {
        private protected readonly BehaviorSubject<Func<TElement,bool>>         FilterStream;  // Filter
        private protected readonly BehaviorSubject<IComparer<TElement>>         SorterStream;  // Sorter
        private protected readonly SourceList<TElement>                         EditableCollection; // Editable
        private protected readonly ReadOnlyObservableCollection<TElement>       BindableCollection; // Bindable

        public DataFactory() : base()
        {
            FilterStream = new BehaviorSubject<Func<TElement, bool>>(x => true);
            SorterStream = new BehaviorSubject<IComparer<TElement>>(Comparer<TElement>.Default);
            EditableCollection = new SourceList<TElement>();
            EditableCollection.Connect()
                              .Filter(FilterStream)
                              .Sort(SorterStream)
                              .Page(PagerStream)
                              .Bind(out BindableCollection)
                              .ObserveOn(OnSetupObserveScheduler())
                              .SubscribeOn(OnSetupSubscribeScheduler())
                              .Subscribe(x => OnSubscribe(x));
        }

        public void ChangeSelector(Func<TElement, bool> filterSelector)
        {
            if (filterSelector == null)
            {
                return;
            }
            FilterStream.OnNext(filterSelector);
        }

        public void ChangeSelector(IComparer<TElement> sortSelector)
        {
            if (sortSelector == null)
            {
                return;
            }
            SorterStream.OnNext(sortSelector);
        }

        public void ChangeSelector(IPageRequest request)
        {
            if (request == null)
            {
                return;
            }
            PagerStream.OnNext(request);
        }

        public virtual void Add(TElement element)
        {
            EditableCollection.Add(element);
        }

        public virtual void AddRange(IEnumerable<TElement> elements)
        {
            EditableCollection.AddRange(elements);
        }

        public virtual bool Remove(TElement element)
        {
            return EditableCollection.Remove(element);
        }

        public virtual void RemoveRange(int index,int count)
        {
            EditableCollection.RemoveRange(index,count);
        }

        public virtual void Clear()
        {
            EditableCollection.Clear();
        }

        protected virtual void OnSubscribe(IChangeSet<TElement> x)
        {

        }

        protected virtual IScheduler OnSetupObserveScheduler()
        {
            return ImmediateScheduler.Instance;
        }

        protected virtual IScheduler OnSetupSubscribeScheduler()
        {
            return ImmediateScheduler.Instance;
        }

        public IObserver<Func<TElement, bool>> Filter => FilterStream;
        public IObserver<IComparer<TElement>> Sorter => SorterStream;
        public ReadOnlyObservableCollection<TElement> Collection => BindableCollection;
    }
}
