using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Reactive
{
    public class DefferObserver<T> : ObserverBase<T>,IObserver<T>
    {
        protected readonly Action<T> Handler;

        public DefferObserver(Action<T> handler)
        {
            Handler = handler ?? throw new InvalidOperationException(nameof(handler));
        }


        protected override void OnCompletedCore()
        {
        }

        protected override void OnErrorCore(Exception error)
        {
        }

        protected override void OnNextCore(T value)
        {
            Handler(value);
        }
    }
}
