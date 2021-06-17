using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Acorisoft.Extensions.ComponentModel
{
    
#pragma warning disable 8632
    
    public abstract class Bindable : INotifyPropertyChanged,INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected bool SetValue<T>(ref T backend, T value, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<T>.Default.Equals(backend, value))
            {
                return false;
            }

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            backend = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }
        
        protected void RaiseUpdated([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected void RaiseUpdating([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}