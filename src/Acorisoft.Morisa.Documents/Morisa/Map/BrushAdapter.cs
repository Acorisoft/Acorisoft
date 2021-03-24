using Acorisoft.Morisa.Core;
using System;

namespace Acorisoft.Morisa.Map
{
    public class BrushAdapter : Bindable, IBrushAdapter
    {
        private bool _IsSelected;

        public BrushAdapter(IBrush x)
        {
            Creation = DateTime.Now;
            Source = x;
        }

        public DateTime Creation { get; set; }
        //
        // TODO:
        public int Id { get; set; }
        public Guid ParentId { get; set; }
        public BrushMode Mode { get; set; }
        public int RefId { get; set; }
        public FillMode Left { get; set; }
        public FillMode Right { get; set; }
        public FillMode Top { get; set; }
        public FillMode Bottom { get; set; }
        public bool IsSelected
        {
            get => _IsSelected;
            set => Set(ref _IsSelected, value);
        }
        public IBrush Source { get; }
    }
}