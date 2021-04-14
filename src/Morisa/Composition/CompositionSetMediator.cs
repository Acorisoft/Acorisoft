using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Acorisoft.Morisa.Composition
{
    public class CompositionSetMediator : Mediator, ICompositionSetMediator
    {
        public CompositionSetMediator(ServiceFactory factory) : base(factory)
        {

        }
    }
}
