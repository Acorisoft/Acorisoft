namespace Acorisoft.Studio.Documents.StickyNote
{
    public class StickyNoteIndexWrapper : DocumentIndexWrapper<StickyNoteIndex>
    {
        public StickyNoteIndexWrapper(StickyNoteIndex index) : base(index)
        {
        }

        public string Name
        {
            get => Source.Name;
            set
            {
                Source.Name = value;
                RaiseUpdated();
            }
        }
        
        public string Summary
        {
            get => Source.Summary;
            set
            {
                Source.Summary = value;
                RaiseUpdated();
            }
        }
    }
}