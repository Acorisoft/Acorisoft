using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class MapGroupAdapter : IMapGroupAdapter
    {
        public MapGroupAdapter(IMapGroup group)
        {

        }

        public ObservableCollection<IMapGroup> Children { get; set; }
    }
}
