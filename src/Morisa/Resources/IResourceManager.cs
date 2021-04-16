using Acorisoft.Morisa.EventBus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Resources
{
    public interface IResourceManager : INotificationHandler<CompositionSetOpeningInstruction>
    {
    }
}
