using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collections
{
    public class ObservableUniqueCollection<T> : ObservableCollection<T>
    {
        [BsonIgnore]
        private readonly HashSet<T> _Hashtable;

        public ObservableUniqueCollection() :base()
        {
            _Hashtable = new HashSet<T>();
        }

        public ObservableUniqueCollection(IEnumerable<T> collection) : base(collection)
        {
            _Hashtable = new HashSet<T>(collection);
        }

        public ObservableUniqueCollection(List<T> list) : base(list)
        {
            _Hashtable = new HashSet<T>(list);
        }

        protected override void ClearItems()
        {
            _Hashtable.Clear();
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            if (_Hashtable.Add(item))
            {
                base.InsertItem(index, item);
            }
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            if (_Hashtable.Remove(Items[index]))
            {
                base.RemoveItem(index);
            }
        }

        protected override void SetItem(int index, T item)
        {
            _Hashtable.Remove(Items[index]);
            if (_Hashtable.Add(item))
            {
                base.SetItem(index, item);
            }
            
        }
    }
}
