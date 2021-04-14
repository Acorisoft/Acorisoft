using System;

namespace Acorisoft.Morisa.Composition
{
#pragma warning disable CA1816 

    public class CompositionSetContext : ActivatingContext<CompositionSet, CompositionSetProperty>, ICompositionSetContext,IDisposable
    {
        public CompositionSetContext(CompositionSet cs, ILoadContext context) : base(cs, context)
        {
        }

        public void Dispose()
        {
            if (!Activating.IsDisposed)
            {
                ((IDisposable)Activating).Dispose();
            }
        }
    }
}