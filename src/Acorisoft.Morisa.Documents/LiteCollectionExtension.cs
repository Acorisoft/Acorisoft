using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft
{
    public static class LiteCollectionExtension
    {
        public static T FindOne<T>(this ILiteCollection<BsonDocument> collection , BsonValue Id)
        {
            var expression = Query.EQ("_id" , Id);
            var document = collection.FindOne(expression).AsDocument;
            return BsonMapper.Global.Deserialize<T>(document);
        }
    }
}
