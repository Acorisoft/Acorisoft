using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.EventBus;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Moniker
{
    public class MonikerSystem : EntitySystem, IMonikerSystem
    {
        private LiteCollection<Moniker> _MonikerCollectionInDatabase;

        protected override Task OnCompositionSetChanged(CompositionSetOpeningInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var context = instruction.Context;
                var activating = context.Activating;
                

            }, cancellationToken);
        }

        protected override Task OnCompositionSetClosing(CompositionSetClosingInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

            }, cancellationToken);
        }
    }
}
