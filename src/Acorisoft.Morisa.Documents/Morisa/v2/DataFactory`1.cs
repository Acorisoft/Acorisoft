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
    /// <summary>
    /// <see cref="DataFactory{TElement}"/> 表示一个数据工厂。为数据提供基础的增删改查以及绑定支持。
    /// </summary>
    /// <typeparam name="TElement">当前数据工厂所维护的数据类型。</typeparam>
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

        /// <summary>
        /// 改变选择器。
        /// </summary>
        /// <param name="filterSelector">指定一个新的集合过滤选择器。</param>
        public void ChangeSelector(Func<TElement, bool> filterSelector)
        {
            if (filterSelector == null)
            {
                return;
            }
            FilterStream.OnNext(filterSelector);
        }

        /// <summary>
        /// 改变选择器。
        /// </summary>
        /// <param name="sortSelector">指定一个新的排序选择器。</param>
        public void ChangeSelector(IComparer<TElement> sortSelector)
        {
            if (sortSelector == null)
            {
                return;
            }
            SorterStream.OnNext(sortSelector);
        }


        /// <summary>
        /// 改变选择器。
        /// </summary>
        /// <param name="request">指定一个新的分页选择器。</param>
        public void ChangeSelector(IPageRequest request)
        {
            if (request == null)
            {
                return;
            }
            PagerStream.OnNext(request);
        }

        /// <summary>
        /// 添加一个新的元素到集合。
        /// </summary>
        /// <param name="element">指定要添加的元素。</param>
        public virtual void Add(TElement element)
        {
            EditableCollection.Add(element);
        }

        /// <summary>
        /// 添加一个新的元素集合。
        /// </summary>
        /// <param name="elements">指定要添加的元素集合。</param>
        public virtual void AddRange(IEnumerable<TElement> elements)
        {
            EditableCollection.AddRange(elements);
        }

        /// <summary>
        /// 将一个元素从当前集合中移除。
        /// </summary>
        /// <param name="element">指定要移除的元素</param>
        /// <returns>返回此次操作的成功标志，如果成功则返回true，否则返回false。</returns>
        public virtual bool Remove(TElement element)
        {
            return EditableCollection.Remove(element);
        }

        /// <summary>
        /// 移除当前集合指定范围的元素。
        /// </summary>
        /// <param name="index">指定移除操作的目标位置。</param>
        /// <param name="count">指定移除操作的元素个数。</param>
        public virtual void RemoveRange(int index,int count)
        {
            EditableCollection.RemoveRange(index,count);
        }

        /// <summary>
        /// 清空当前集合。
        /// </summary>
        public virtual void Clear()
        {
            EditableCollection.Clear();
        }

        /// <summary>
        /// 响应当前集合发生的改变。
        /// </summary>
        /// <param name="x">当前发生改变的集合。</param>
        protected virtual void OnSubscribe(IChangeSet<TElement> x)
        {

        }

        /// <summary>
        /// 配置当前集合变化的观测线程。
        /// </summary>
        /// <returns>返回一个非空的调度器。</returns>
        protected virtual IScheduler OnSetupObserveScheduler()
        {
            return ImmediateScheduler.Instance;
        }

        /// <summary>
        /// 配置当前集合变化的订阅线程。
        /// </summary>
        /// <returns>返回一个非空的调度器。</returns>
        protected virtual IScheduler OnSetupSubscribeScheduler()
        {
            return ImmediateScheduler.Instance;
        }

        /// <summary>
        /// 获取一个过滤器流。这个流用于设置过滤器
        /// </summary>
        public IObserver<Func<TElement, bool>> Filter => FilterStream;

        /// <summary>
        /// 获取一个排序流，这个流用于设置排序功能。
        /// </summary>
        public IObserver<IComparer<TElement>> Sorter => SorterStream;

        /// <summary>
        /// 获取一个可观测的集合。这个集合用于为视图提供绑定支持。
        /// </summary>
        public ReadOnlyObservableCollection<TElement> Collection => BindableCollection;
    }
}
