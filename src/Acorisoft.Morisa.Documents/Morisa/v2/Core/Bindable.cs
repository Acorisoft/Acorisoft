using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Acorisoft.Morisa.v2.Core
{
    public abstract class Bindable : INotifyPropertyChanged, INotifyPropertyChanging
    {        
        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        [BsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangedEventHandler ChangedHandler;

        [BsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangingEventHandler ChangingHandler;

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        public Bindable()
        {
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        protected bool Set<T>(ref T backendField, T value, [CallerMemberName] string name = "")
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

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

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
