using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public enum BinaryObjectType
    {
        Image,
        Binary
    }

    public abstract class BinaryObject
    {
        [BsonId]
        public string Md5 { get; set; }
        public string Name { get; set; }

        [BsonIgnore]
        public string FileName { get; set; }
        public abstract BinaryObjectType Type { get; }
    }

    public sealed class ImageObject : BinaryObject
    {
        public override sealed BinaryObjectType Type => BinaryObjectType.Image;
    }
}
