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
    /// <summary>
    /// <see cref="CollectionWrapper{TElement, TList}"/> 类型用于封装现有的可观测集合类型来为用户提供过滤器支持。
    /// </summary>
    /// <typeparam name="TElement">表示存放于集合的元素类型。</typeparam>
    /// <typeparam name="TList">表示封装的可观测集合类型。</typeparam>
    public class CollectionWrapper<TElement, TList> : IReadOnlyCollection<TElement>, INotifyCollectionChanged, INotifyPropertyChanged where TList : IList<TElement>, INotifyCollectionChanged
    {
        private protected PropertyChangedEventHandler _PropertyChanged;
        private protected NotifyCollectionChangedEventHandler _CollectionChanged;
        private protected ICollectionPredicator _Predicator;
        private protected string _Keyword;

        private class DefaultCollectionPredicator : CollectionPredicator
        {
            public override sealed bool Predicate(object element)
            {
                return true;
            }
        }

        private struct CollectionWrapperEnumeartor : IEnumerator<TElement>
        {
            private IEnumerator<TElement> _Iterator;
            private ICollectionPredicator _Predicator;
            private TElement _Element;

            public CollectionWrapperEnumeartor(CollectionWrapper<TElement, TList> collectionWrappers, ICollectionPredicator predicator)
            {
                _Iterator = collectionWrappers.List.GetEnumerator();
                _Predicator = predicator;
                _Element = default(TElement);
            }

            public TElement Current => _Element;
            object IEnumerator.Current => _Element;

            public void Dispose()
            {
                _Iterator.Dispose();
            }

            public bool MoveNext()
            {
                while(_Iterator.MoveNext())
                {
                    while (!_Predicator.Predicate(_Iterator.Current) && _Iterator.MoveNext())
                    {
                    }

                    _Element = _Iterator.Current;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _Iterator.Reset();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public CollectionWrapper(TList list) : this(list, new DefaultCollectionPredicator())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicator"></param>
        public CollectionWrapper(TList list, ICollectionPredicator predicator)
        {
            List = list ?? throw new ArgumentNullException(nameof(list));
            _Predicator = predicator;
            Connect(List);
        }

        public void Connect(TList list)
        {
            if (list is not INotifyCollectionChanged)
            {
                return;
            }

            list.CollectionChanged += OnCollectionChanged;
        }

        public void Disconnect(TList list)
        {
            if (list is not INotifyCollectionChanged)
            {
                return;
            }

            list.CollectionChanged -= OnCollectionChanged;
        }

        protected void OnCollectionReset()
        {
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));            
        }

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _CollectionChanged?.Invoke(sender, e);
        }


        public IEnumerator<TElement> GetEnumerator()
        {
            return new CollectionWrapperEnumeartor(this, _Predicator);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CollectionWrapperEnumeartor(this, _Predicator);
        }

        /// <summary>
        /// 获取当前集合的元素个数。
        /// </summary>
        public int Count => List.Count;

        /// <summary>
        /// 获取当前封装的集合。
        /// </summary>
        public TList List { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICollectionPredicator Predicator {
            get => _Predicator;
            set {
                if (value is not null && !ReferenceEquals(value, _Predicator))
                {
                    _Predicator = value;
                    OnCollectionReset();
                }
            }
        }

        public string Keyword {
            get => _Keyword;
            set {
                if(!string.IsNullOrEmpty(value) && value != _Keyword)
                {
                    _Keyword = value;
                    _Predicator.Keyword = value;
                    OnCollectionReset();
                }
            }
        }


        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{
            add => _PropertyChanged += value;
            remove => _PropertyChanged -= value;
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged{
            add => _CollectionChanged += value;
            remove => _CollectionChanged -= value;
        }
    }
}
