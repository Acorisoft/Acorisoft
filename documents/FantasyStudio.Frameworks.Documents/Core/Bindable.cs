using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Core
{
    public abstract class Bindable : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private PropertyChangedEventHandler Changed;
        private PropertyChangingEventHandler Changing;

        protected bool SetValue<T>(ref T backend,T value,[CallerMemberName]string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backend, value))
            {
                Changing?.Invoke(this, new PropertyChangingEventArgs(name));
                backend = value;
                Changed?.Invoke(this, new PropertyChangedEventArgs(name));
                return true;
            }

            return false;
        }

        protected void RaiseUpdated(string name)
        {
            Changed?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void RaiseUpdating(string name)
        {
            Changing?.Invoke(this, new PropertyChangingEventArgs(name));
        }

        [BsonIgnore]
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => Changed += value;
            remove => Changed -= value;
        }

        [BsonIgnore]
        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add => Changing += value;
            remove => Changing -= value;
        }
    }
}
