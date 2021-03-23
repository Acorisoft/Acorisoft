using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Acorisoft.Morisa.Map
{
    public interface IBrushGroupTree : INotifyPropertyChanged
    {
        public Guid Id { get; }
        public Guid ParentId { get; }
        public IBrushGroup Source { get; }
        public ReadOnlyObservableCollection<IBrushGroupTree> Children { get; }
        public string Name
        {
            get; set;
        }
    }
}
