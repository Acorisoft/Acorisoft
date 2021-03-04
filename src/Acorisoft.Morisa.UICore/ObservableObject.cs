using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class ObservableObject : INotifyPropertyChanging, INotifyPropertyChanged
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangedEventHandler ChangedHandler;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangingEventHandler ChangingHandler;

        protected bool SetValueAndRaiseUpdate<T>(ref T backendField, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backendField, value))
            {
                ChangingHandler?.Invoke(this, new PropertyChangingEventArgs(name));
                backendField = value;
                ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
                return true;
            }

            return false;
        }

        protected void RaiseUpdate([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => ChangedHandler += value;
            remove => ChangedHandler -= value;
        }

        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add => ChangingHandler += value;
            remove => ChangingHandler -= value;
        }
    }
}
