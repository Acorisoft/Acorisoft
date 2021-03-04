using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collections
{
    public class CollectionPredicator : ObservableObject, ICollectionPredicator
    {
        private string _key;

        public virtual bool Predicate(object element)
        {
            return ContainsKeyword(element) && true;
        }

        protected bool ContainsKeyword(object element)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                return true;
            }

            return !string.IsNullOrEmpty(Keyword) && ContainsKeywordCore(element);
        }

        protected virtual bool ContainsKeywordCore(object element)
        {
            return true;
        }

        public string Keyword { get; set; }

        public string Key {
            get => _key;
            set => SetValueAndRaiseUpdate(ref _key, value);
        }
    }
}
