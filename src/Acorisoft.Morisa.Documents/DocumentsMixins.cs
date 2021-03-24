using Acorisoft.Morisa.Map;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft
{
    public static class DocumentsMixins
    {
        public static IContainer UseMorisa(this IContainer container)
        {
            container.RegisterInstance<IBrushSetFactory>(new BrushSetFactory());
            return container;
        }
    }
}
