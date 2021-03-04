using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collections
{
    public class DelegateCollectionPredicator : CollectionPredicator, ICollectionPredicator
    {
        private Predicate<object> _handler;

        public DelegateCollectionPredicator(Predicate<object> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public override sealed bool Predicate(object element)
        {
            return _handler(element);
        }
    }
}
