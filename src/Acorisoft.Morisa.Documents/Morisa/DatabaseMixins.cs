using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public static class DatabaseMixins
    {
        public static readonly BsonMapper Mapper = BsonMapper.Global;

        public static T Deserialize<T>(this BsonDocument document)
        {
            return Mapper.ToObject<T>(document);
        }

        public static BsonDocument Serialize<T>(T value)
        {
            return Mapper.ToDocument<T>(value);
        }
    }
}
