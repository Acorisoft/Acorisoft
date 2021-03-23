using Acorisoft.Morisa.Core;
using System;

namespace Acorisoft.Morisa.Map
{
    public class BrushAdapter : Bindable, IBrushAdapter
    {
        public BrushAdapter(IBrush x)
        {
        }

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
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IBrush Source => throw new NotImplementedException();
    }
}