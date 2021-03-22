using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class CompositionSetInformation : ICompositionSetInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string Topic { get; set; }

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
