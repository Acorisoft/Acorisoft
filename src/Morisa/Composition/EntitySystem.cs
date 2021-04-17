using Acorisoft.Morisa.EventBus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Composition
{
    public abstract class EntitySystem : IEntitySystem
    {
        protected abstract Task OnCompositionSetChanged(CompositionSetOpeningInstruction instruction, CancellationToken cancellationToken);
        protected abstract Task OnCompositionSetClosing(CompositionSetClosingInstruction instruction, CancellationToken cancellationToken);

        Task INotificationHandler<CompositionSetOpeningInstruction>.Handle(CompositionSetOpeningInstruction notification, CancellationToken cancellationToken)
        {
            return OnCompositionSetChanged(notification, cancellationToken);
        }

        Task INotificationHandler<CompositionSetClosingInstruction>.Handle(CompositionSetClosingInstruction notification, CancellationToken cancellationToken)
        {
            return OnCompositionSetClosing(notification, cancellationToken);
        }
    }
}
