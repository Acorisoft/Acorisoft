using LiteDB;

namespace Acorisoft.Studio
{
    internal static class Helper
    {
        internal const string Id = "_id";
        
        public static bool Exists<T>(this LiteCollection<T> collection, string key)
        {
            return collection.Exists(Query.EQ(Id, new BsonValue(key)));
        }
    }
}