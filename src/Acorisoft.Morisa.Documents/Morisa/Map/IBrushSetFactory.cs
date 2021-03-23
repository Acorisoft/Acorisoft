using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IBrushSetFactory : IDataSetFactory<BrushSet, BrushSetProperty>
    {
        ReadOnlyObservableCollection<Node<IBrushGroup, Guid>> BrushGroups { get; }
    }
}
