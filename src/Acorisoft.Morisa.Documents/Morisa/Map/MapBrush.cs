using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class MapBrush : IMapBrush
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Resource Brush { get; set; }

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
