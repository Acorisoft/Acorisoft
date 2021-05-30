using System;
using Acorisoft.Studio.ProjectSystem;
using LiteDB;
using MediatR;

namespace Acorisoft.Studio
{
    [Obsolete]
    public class CompositionSetOpenNotification : INotification
    {
        internal CompositionSetOpenNotification(CompositionSet compositionSet)
        {
            CompositionSet = compositionSet ?? throw new ArgumentNullException(nameof(compositionSet));
        }

        internal LiteDatabase MainDatabase => (CompositionSet as ICompositionSetDatabase)?.MainDatabase;
        public ICompositionSet CompositionSet { get; }
    }
}