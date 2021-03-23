using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="BrushGroup"/> 表示一个画刷分组。
    /// </summary>
    public class BrushGroup : Bindable, IBrushGroup
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public string Name { get; set; }
    }
}
