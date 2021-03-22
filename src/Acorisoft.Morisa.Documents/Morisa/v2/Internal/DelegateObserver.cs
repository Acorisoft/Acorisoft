using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Internal
{
#pragma warning disable IDE0034

    public class DelegateObserver<T> : ObserverBase<T>, IObserver<T>
    {
        private readonly Predicate<T>   _Predicate;
        private readonly Action<T>      _Handler;
        private T       _Value;
        private bool    _IsChanged;

        public DelegateObserver(Action<T> handler) : this(Delegate<T>.AlmostTrue, handler)
        {

        }

        public DelegateObserver(Predicate<T> predicate, Action<T> handler)
        {
            _Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }


        protected override void OnCompletedCore()
        {
            if (_IsChanged)
            {
                _IsChanged = false;
                _Handler(_Value);
            }
        }

        protected override void OnErrorCore(Exception error)
        {
            if (error != null)
            {
                _IsChanged = false;
                _Value = default(T);
            }
        }

        protected override void OnNextCore(T value)
        {
            if (_Predicate(value))
            {
                _Value = value;
                _Handler(_Value);
            }
        }
    }
}
