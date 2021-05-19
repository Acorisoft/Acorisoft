using System.ComponentModel;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        string Title { get; }
    }
}