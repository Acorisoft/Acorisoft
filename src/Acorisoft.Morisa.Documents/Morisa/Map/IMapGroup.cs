using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public partial interface IMapGroup
    {
        Guid Id { get; set; }
        Guid? OwnerId { get; set; }
        string Name { get; set; }

    }
}
