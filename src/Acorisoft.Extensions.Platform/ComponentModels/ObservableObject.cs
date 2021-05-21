using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acorisoft.Extension.Annotations;

namespace Acorisoft.Extension.ComponentModels
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetValue<T>(ref T backend, T value, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<T>.Default.Equals(backend, value))
            {
                return false;
            }

            backend = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }
        
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}