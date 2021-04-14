using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Emblem
{
    public interface IEmblemSystem : IEntitySystem, IDataFactory<IEmblem>
    {
    }
}
