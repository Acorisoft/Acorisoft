using System;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Core;

namespace Acorisoft.Studio.Engines
{
    [Obsolete]
    public interface IStickyNoteEngine : IComposeSetSystemModule<InspirationIndex, InspirationIndexWrapper, StickyNoteInspiration>, IComposeSetSystemModule
    {
        
    }
}