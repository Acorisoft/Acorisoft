using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Internal
{
    public class ImmediateDelegateRecipient<T> : IObserver<T>
    {
        private readonly Predicate<T>   _Predicate;
        private readonly Action<T>      _Handler;

        public ImmediateDelegateRecipient(Action<T> handler) : this(Delegate<T>.AlmostTrue, handler)
        {

        }

        public ImmediateDelegateRecipient(Predicate<T> predicate, Action<T> handler)
        {
            _Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }


        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            if(error != null)
            {
                
            }
        }

        public void OnNext(T value)
        {
            if (_Predicate(value))
            {
                _Handler(value);
            }
        }
    }
}
