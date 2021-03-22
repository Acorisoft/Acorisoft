using Acorisoft.Morisa.Core;
using System.Collections.Generic;

namespace Acorisoft.Morisa.v2.Map
{
    public class MapBrushSetInformation : IMapBrushSetInformation
    {
        public MapBrushSetInformation()
        {
            Tags = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Tags { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Authors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Resource Cover { get; set; }
    }
}