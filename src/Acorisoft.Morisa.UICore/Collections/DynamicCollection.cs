using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collections
{
	// System.Collections.ObjectModel.ObservableCollection<T>
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Threading;

	[Serializable]	
	public class DynamicCollection<TElement> : Collection<TElement>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		[Serializable]
		private class SimpleMonitor : IDisposable
		{
			private int _busyCount;

			public bool Busy => _busyCount > 0;

			public void Enter()
			{
				_busyCount++;
			}

			public void Dispose()
			{
				_busyCount--;
			}
		}

		private const string CountString = "Count";

		private const string IndexerName = "Item[]";

		private SimpleMonitor _monitor = new SimpleMonitor();

		
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			
			add
			{
				PropertyChanged += value;
			}
			
			remove
			{
				PropertyChanged -= value;
			}
		}

		
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged
		{
			[CompilerGenerated]
			
			add
			{
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = this.CollectionChanged;
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler2;
				do
				{
					notifyCollectionChangedEventHandler2 = notifyCollectionChangedEventHandler;
					NotifyCollectionChangedEventHandler value2 = (NotifyCollectionChangedEventHandler)Delegate.Combine(notifyCollectionChangedEventHandler2, value);
					notifyCollectionChangedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, value2, notifyCollectionChangedEventHandler2);
				}
				while ((object)notifyCollectionChangedEventHandler != notifyCollectionChangedEventHandler2);
			}
			[CompilerGenerated]
			
			remove
			{
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = this.CollectionChanged;
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler2;
				do
				{
					notifyCollectionChangedEventHandler2 = notifyCollectionChangedEventHandler;
					NotifyCollectionChangedEventHandler value2 = (NotifyCollectionChangedEventHandler)Delegate.Remove(notifyCollectionChangedEventHandler2, value);
					notifyCollectionChangedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, value2, notifyCollectionChangedEventHandler2);
				}
				while ((object)notifyCollectionChangedEventHandler != notifyCollectionChangedEventHandler2);
			}
		}

		
		protected virtual event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		
		public ObservableCollection()
		{
		}

		public ObservableCollection(List<T> list)
			: base((IList<T>)((list != null) ? new List<T>(list.Count) : list))
		{
			CopyFrom(list);
		}

		
		public ObservableCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			CopyFrom(collection);
		}

		private void CopyFrom(IEnumerable<T> collection)
		{
			IList<T> items = base.Items;
			if (collection != null && items != null)
			{
				foreach (T item in collection)
				{
					items.Add(item);
				}
			}
		}

		
		public void Move(int oldIndex, int newIndex)
		{
			MoveItem(oldIndex, newIndex);
		}

		
		protected override void ClearItems()
		{
			CheckReentrancy();
			base.ClearItems();
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionReset();
		}

		
		protected override void RemoveItem(int index)
		{
			CheckReentrancy();
			T val = base[index];
			base.RemoveItem(index);
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, val, index);
		}

		
		protected override void InsertItem(int index, T item)
		{
			CheckReentrancy();
			base.InsertItem(index, item);
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		
		protected override void SetItem(int index, T item)
		{
			CheckReentrancy();
			T val = base[index];
			base.SetItem(index, item);
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Replace, val, item, index);
		}

		
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			CheckReentrancy();
			T val = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, val);
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Move, val, newIndex, oldIndex);
		}

		
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}

		
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (BlockReentrancy())
				{
					this.CollectionChanged(this, e);
				}
			}
		}

		
		protected IDisposable BlockReentrancy()
		{
			_monitor.Enter();
			return _monitor;
		}

		
		protected void CheckReentrancy()
		{
			if (_monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
			{
				throw new InvalidOperationException(SR.GetString("ObservableCollectionReentrancyNotAllowed"));
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		private void OnCollectionReset()
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}

}
