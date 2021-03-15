using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public class OutstandResource : Resource
    {
        public string FileName { get; set; }

        [BsonIgnore]
        public bool IsShadowCopy { get; set; }
    }
}
