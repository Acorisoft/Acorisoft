using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{
    public interface ITagCollection : IList<ITag>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}
