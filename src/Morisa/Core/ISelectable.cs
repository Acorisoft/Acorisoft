using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface ISelectable : IBindable
    {
        bool IsSelected { get; set; }
        bool CanSelected { get; set; }
    }
}
