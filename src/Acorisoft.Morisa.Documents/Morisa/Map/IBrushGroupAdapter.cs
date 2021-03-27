using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Acorisoft.Morisa.Map
{
    public interface IBrushGroupAdapter : INotifyPropertyChanged
    {
        public bool IsLocked { get; set; }
        public Guid Id { get; }
        public Guid ParentId { get; }
        public IBrushGroup Source { get; }
        public bool IsElement { get; set; }
        public ReadOnlyObservableCollection<BrushGroupAdapter> Children { get; }
        public string Name
        {
            get; set;
        }
    }
}
