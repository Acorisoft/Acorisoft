using System.Collections;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Studio.Documents.StickyNotes;

namespace Acorisoft.Studio.ViewModels
{
    public sealed class StickyNoteParameter : Hashtable
    {
        public StickyNoteParameter(StickyNoteIndexWrapper wrapper,StickyNoteIndex index, StickyNoteDocument document)
        {
            Add(ViewAware.Arg1, document);
            Add(ViewAware.Arg2, index);
            Add(ViewAware.Arg3, wrapper);
        }
        
        public StickyNoteDocument Document => base[ViewAware.Arg1] as StickyNoteDocument;
        public StickyNoteIndex Index => base[ViewAware.Arg2] as StickyNoteIndex;
        public StickyNoteIndexWrapper Wrapper => base[ViewAware.Arg3] as StickyNoteIndexWrapper;
    }
}