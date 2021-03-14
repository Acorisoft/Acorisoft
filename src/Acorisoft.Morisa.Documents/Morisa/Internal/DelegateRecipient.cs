using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Internal
{
#pragma warning disable IDE0034

    public class DelegateRecipient<T> : IObserver<T>
    {
        private readonly Predicate<T>   _Predicate;
        private readonly Action<T>      _Handler;
        private T       _Value;
        private bool    _IsChanged;

        public DelegateRecipient(Action<T> handler) : this(Delegate<T>.AlmostTrue, handler)
        {

        }

        public DelegateRecipient(Predicate<T> predicate, Action<T> handler)
        {
            _Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }


        public void OnCompleted()
        {
            if (_IsChanged)
            {
                _IsChanged = false;
                _Handler(_Value);
            }
        }

        public void OnError(Exception error)
        {
            if (error != null)
            {
                _IsChanged = false;
                _Value = default(T);
            }
        }

        public void OnNext(T value)
        {
            if (_Predicate(value))
            {
                _Value = value;
                _IsChanged = true;
            }
        }
    }
}
