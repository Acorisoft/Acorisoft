using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IBrush
    {
        /// <summary>
        /// 
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Guid ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BrushMode Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int RefId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Left { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Right { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode Bottom { get; set; }
    }
}
