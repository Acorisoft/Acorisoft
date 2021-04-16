using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="Bindable"/> 类型表示一个可绑定对象。用于提供对象的视图绑定功能。
    /// </summary>
    public abstract class Bindable : IBindable
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
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
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        
        public Bindable()
        {
        }
        
        //-------------------------------------------------------------------------------------------------
        //
        //  Protected Methods
        //
        //-------------------------------------------------------------------------------------------------

        protected bool SetValue<T>(ref T backendField, T value, [CallerMemberName] string name = "")
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

        protected void RaiseUpdating([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void RaiseUpdated([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Explicit Events
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
