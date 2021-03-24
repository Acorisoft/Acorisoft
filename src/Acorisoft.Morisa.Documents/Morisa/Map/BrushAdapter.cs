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

        /// <summary>
        /// 
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //
        // TODO:
        public int Id
        {
            get => Source.Id;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid ParentId
        {
            get => Source.ParentId;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BrushMode Mode
        {
            get => Source.Mode;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RefId
        {
            get => Source.RefId;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Left
        {
            get => Source.Left;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Right
        {
            get => Source.Right;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Top
        {
            get => Source.Top;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Bottom
        {
            get => Source.Bottom;
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IBrush Source { get; }
    }
}