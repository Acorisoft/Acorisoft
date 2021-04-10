using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    [DebuggerDisplay("Count = {_Dict.Count}")]
    public class NavigateParameter : INavigateParameter, IReadOnlyDictionary<string, object>
    {
        private readonly Dictionary<string,object> _Dict;

        public NavigateParameter()
        {
            _Dict = new Dictionary<string, object>();
        }

        public NavigateParameter(IDictionary<string,object> source)
        {
            _Dict = new Dictionary<string, object>(source);
        }

        public NavigateParameter(IEnumerable<KeyValuePair<string,object>> source)
        {
            _Dict = new Dictionary<string, object>(source);
        }

        public void Set(string key,object value)
        {
            _Dict.TryAdd(key, value);
        }

        #region IReadOnlyDictionary<string, object> Interface Implementations

        public object this[string key] => ((IReadOnlyDictionary<string, object>)_Dict)[key];

        public IEnumerable<string> Keys => ((IReadOnlyDictionary<string, object>)_Dict).Keys;

        public IEnumerable<object> Values => ((IReadOnlyDictionary<string, object>)_Dict).Values;

        public int Count => ((IReadOnlyCollection<KeyValuePair<string, object>>)_Dict).Count;

        public bool ContainsKey(string key)
        {
            return ((IReadOnlyDictionary<string, object>)_Dict).ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)_Dict).GetEnumerator();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            return ((IReadOnlyDictionary<string, object>)_Dict).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_Dict).GetEnumerator();
        }

        #endregion IReadOnlyDictionary<string, object> Interface Implementations

        public override sealed string ToString()
        {
            return $"Count:{_Dict.Count}";
        }
    }
}
