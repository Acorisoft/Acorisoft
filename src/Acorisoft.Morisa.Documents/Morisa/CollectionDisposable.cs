using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class CollectionDisposable<T> : IDisposable
    {
        private readonly ICollection<T> _source;
        private readonly T _element;

        public CollectionDisposable(ICollection<T> source, T element)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _element = element ?? throw new ArgumentNullException(nameof(element));
        }

        [SuppressMessage("Usage", "CA1816:Dispose 方法应调用 SuppressFinalize", Justification = "<挂起>")]
        public void Dispose()
        {
            _source.Remove(_element);
        }
    }
}
