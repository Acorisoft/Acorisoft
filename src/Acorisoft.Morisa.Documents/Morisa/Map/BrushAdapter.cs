using Acorisoft.Morisa.Core;
using System;

namespace Acorisoft.Morisa.Map
{
    public class BrushAdapter : Selectable, IBrushAdapter
    {
        public BrushAdapter(IBrush x)
        {
            Creation = DateTime.Now;
            Source = x;
        }

        public DateTime Creation { get; set; }
        //
        // TODO:
        public int Id
        {
            get => Source.Id;
            set
            {

            }
        }

        public Guid ParentId
        {
            get => Source.ParentId;
            set
            {

            }
        }
        public BrushMode Mode
        {
            get => Source.Mode;
            set
            {

            }
        }
        public int RefId
        {
            get => Source.RefId;
            set
            {

            }
        }
        public FillMode Left
        {
            get => Source.Left;
            set
            {

            }
        }
        public FillMode Right
        {
            get => Source.Right;
            set
            {

            }
        }
        public FillMode Top
        {
            get => Source.Top;
            set
            {

            }
        }
        public FillMode Bottom
        {
            get => Source.Bottom;
            set
            {

            }
        }
        public IBrush Source { get; }
    }
}