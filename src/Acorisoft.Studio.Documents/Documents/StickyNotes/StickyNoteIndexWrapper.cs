using System;

namespace Acorisoft.Studio.Documents.StickyNotes
{
    public class StickyNoteIndexWrapper : Acorisoft.Studio.DocumentIndexWrapper<StickyNoteIndex>, IComparable<StickyNoteIndexWrapper>
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

        public int CompareTo(StickyNoteIndexWrapper y)
        {
            var x = this.Source;
            
            if (x == null || y == null)
            {
                return 0;
            }

            var xTicks = x.CreationTimestamp.Ticks;
            var yTicks = y.Source.CreationTimestamp.Ticks;

            if (xTicks > yTicks)
            {
                return -1;
            }

            return xTicks < yTicks ? 1 : 0;
        }
    }
}