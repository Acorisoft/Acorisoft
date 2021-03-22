using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2
{
    public interface ITransformedDataFactory<TElement, TDestElement> : IDataFactory, IDataFactorySelectable<TDestElement>
    {
        void Add(TElement element);
        void AddRange(IEnumerable<TElement> elements);
        bool Remove(TElement element);
        void RemoveRange(int index, int count);
        void Clear();

        Func<TElement, TDestElement> Transform { get; set; }
        IObserver<Func<TDestElement, bool>> Filter { get; }
        IObserver<IComparer<TDestElement>> Sorter { get; }
        IObserver<IPageRequest> Pager { get; }
        ReadOnlyObservableCollection<TDestElement> Collection { get; }
    }
}
