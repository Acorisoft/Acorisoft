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

        public static T Singleton<T>(this DataSet set)
        {
            var key = typeof(T).FullName;
            T instance;
            if (set.DB_External.Exists(Query.EQ("_id", key)))
            {
                instance = DatabaseMixins.Deserialize<T>(set.DB_External.FindById(key));
            }
            else
            {
                instance = ClassActivator.CreateInstance<T>();
                set.DB_External.Upsert(key, DatabaseMixins.Serialize(instance));
            }

            return instance;
        }
    }
}
