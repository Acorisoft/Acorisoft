using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public static class MorisaMixins
    {
        public static IContainer UseMorisa(this IContainer container)
        {
            container.RegisterInstance<IMorisaProjectManager>(new MorisaProjectManager() , IfAlreadyRegistered.Keep);
            return container;
        }
    }
}
