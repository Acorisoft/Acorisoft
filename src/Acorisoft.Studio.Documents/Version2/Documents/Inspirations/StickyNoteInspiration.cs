using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.Documents.Inspirations
{
    public class StickyNoteInspiration : InspirationDocument
    {
        public ImageResource Album { get; set; }
        public string Content { get; set; }
        public sealed override InspirationType Type => InspirationType.StickyNote;
    }
}