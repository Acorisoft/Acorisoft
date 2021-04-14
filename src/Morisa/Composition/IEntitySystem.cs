using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.EventBus;
using MediatR;

namespace Acorisoft.Morisa.Composition
{
    public interface IEntitySystem : INotificationHandler<CompositionSetOpeningInstruction>
    {
    }
}
