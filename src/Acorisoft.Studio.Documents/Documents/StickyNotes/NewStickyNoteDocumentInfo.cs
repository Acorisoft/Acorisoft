using Acorisoft.Studio.Engines;

namespace Acorisoft.Studio.Documents.StickyNotes
{
    public class NewStickyNoteDocumentInfo : INewDocumentInfo<StickyNoteDocument>
    {
        public string Name { get; set; }
    }
}