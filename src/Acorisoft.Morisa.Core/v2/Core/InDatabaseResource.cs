using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public class InDatabaseResource : Resource
    {

        [BsonIgnore]
        public string FileName { get; set; }
    }
}
