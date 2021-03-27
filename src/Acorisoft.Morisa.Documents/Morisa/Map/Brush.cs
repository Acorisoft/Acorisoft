using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class Brush : IBrush
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonField("p")]
        public Guid ParentId  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonField("l")]
        public FillMode Left  { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [BsonField("r")] 
        public FillMode Right  { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [BsonField("t")] 
        public FillMode Top  { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [BsonField("b")]
        public FillMode Bottom  { get; set; }
    }
}
