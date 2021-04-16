using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Composition;
using MediatR;

namespace Acorisoft.Morisa.EventBus
{
    public class CompositionSetOpeningInstruction : INotification
    {
        public CompositionSetContext Context { get; set; }
    }
}
