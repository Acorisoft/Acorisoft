using Acorisoft.Morisa.Core;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class BrushGroupTree : Bindable, IBrushGroupTree
    {
        private string _GroupName;

        public BrushGroupTree(Node<IBrushGroup,Guid> node)
        {

        }

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public string Name
        {
            get => _GroupName;
            set => Set(ref _GroupName, value);
        }
    }
}
