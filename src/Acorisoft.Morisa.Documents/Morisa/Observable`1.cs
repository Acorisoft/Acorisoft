using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace Acorisoft.Morisa
{
    public class Observable<T> : IObservable<T>,IDisposable
    {
        protected class CollectionDisposable : IDisposable
        {
            readonly List<IObserver<T>> _List;
            readonly IObserver<T> _Element;

            public CollectionDisposable(List<IObserver<T>> list, IObserver<T> element)
            {
                _List = list;
                _Element = element;
            }

            public void Dispose()
            {
                _List.Remove(_Element);
            }
        }

        private protected readonly List<IObserver<T>> _List;

        public Observable()
        {
            _List = new List<IObserver<T>>();
        }


        public IDisposable Subscribe(IObserver<T> observer)
        {
            if(observer == null)
            {
                return Disposable.Empty;
            }

            _List.Add(observer);
            return new CollectionDisposable(_List , observer);
        }

        public void Dispose()
        {
            _List.Clear();
        }
    }
}
