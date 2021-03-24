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
using ReactiveUI;
using Splat;
using DryIoc;

namespace Acorisoft.Morisa.v2
{
    public abstract class TransformedDataFactory<TElement, TDestElement> : DataFactory, ITransformedDataFactory<TElement, TDestElement>
    {
        private protected readonly BehaviorSubject<Func<TDestElement,bool>>         FilterStream;  // Filter
        private protected readonly BehaviorSubject<IComparer<TDestElement>>         SorterStream;  // Sorter
        private protected readonly Func<TElement,TDestElement>                      Transformer;   // Transform
        private protected readonly SourceList<TElement>                             EditableCollection; // Editable
        private protected readonly ReadOnlyObservableCollection<TDestElement>       BindableCollection; // Bindable

        public TransformedDataFactory() : base()
        {
            FilterStream = new BehaviorSubject<Func<TDestElement, bool>>(x => true);
            SorterStream = new BehaviorSubject<IComparer<TDestElement>>(Comparer<TDestElement>.Default);
            Transformer = OnTransform;
            EditableCollection = new SourceList<TElement>();
            EditableCollection.Connect()
                              .Transform(Transformer)
                              .Filter(FilterStream)
                              .Sort(SorterStream)
                              .Page(PagerStream)
                              .Bind(out BindableCollection)
                              .ObserveOn(OnSetupObserveScheduler())
                              .SubscribeOn(OnSetupSubscribeScheduler())
                              .Subscribe(x => OnSubscribe(x));
        }

        public void ChangeSelector(Func<TDestElement, bool> filterSelector)
        {
            if (filterSelector == null)
            {
                return;
            }
            FilterStream.OnNext(filterSelector);
        }

        public void ChangeSelector(IComparer<TDestElement> sortSelector)
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

        public virtual void RemoveRange(int index, int count)
        {
            EditableCollection.RemoveRange(index, count);
        }

        public virtual void Clear()
        {
            EditableCollection.Clear();
        }

        protected TDestElement OnTransform(TElement element)
        {
            if (Transform != null)
            {
                return Transform(element);
            }

            return default;
        }

        protected virtual void OnSubscribe(IChangeSet<TDestElement> x)
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

        public Func<TElement, TDestElement> Transform { get; set; }
        public IObserver<Func<TDestElement, bool>> Filter => FilterStream;
        public IObserver<IComparer<TDestElement>> Sorter => SorterStream;
        public IObserver<IPageRequest> Pager => PagerStream;
        public ReadOnlyObservableCollection<TDestElement> Collection => BindableCollection;
    }
}
