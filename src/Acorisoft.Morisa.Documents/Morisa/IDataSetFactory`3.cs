using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;

namespace Acorisoft.Morisa
{
    interface IDataSetFactoryImpl<TElement, TDataSet, TProperty> : IDataSetFactory<TDataSet, TProperty>
        where TElement : IComposition
        where TDataSet : DataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
        SourceList<TElement> Source { get; }
    }

    public interface IDataSetFactory<TElement, TDataSet, TProperty> : IDataSetFactory<TDataSet, TProperty>
        where TElement : IComposition
        where TDataSet : DataSet<TProperty>
        where TProperty : DataSetProperty, IDataSetProperty
    {
        void Edit(Action<IExtendedList<TElement>> list);
        void Add(TElement element);
        void AddRange(IEnumerable<TElement> elements);
        bool Remove(TElement element);
        void RemoveRange(int index, int count);
        void Clear();

        /// <summary>
        /// 获取一个过滤器流。这个流用于设置过滤器
        /// </summary>
        IObserver<Func<TElement, bool>> Filter { get; }

        /// <summary>
        /// 获取一个排序流，这个流用于设置排序功能。
        /// </summary>
        IObserver<IComparer<TElement>> Sorter { get; }

        ReadOnlyObservableCollection<TElement> Collection { get; }
    }
}
