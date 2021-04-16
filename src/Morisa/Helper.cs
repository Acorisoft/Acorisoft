using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    static class Helper
    {
        public static Guid ToGuid() => Guid.NewGuid();
        public static string ToId => ToGuid().ToString("N");

        public static LiteDatabase ToDatabase(ILoadContext context)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = context.FileName
            });
        }

        public static LiteDatabase ToDatabase(ISaveContext context)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = context.FileName,
                InitialSize = Constants.InitialSize,
                Mode = FileMode.Exclusive
            });
        }

        public static LiteCollection<BsonDocument> ToObject(LiteDatabase database)
        {
            return database.GetCollection(Constants.ObjectCollection);
        }

        public static bool Exists<T>(this LiteCollection<T> collection, BsonValue id)
        {
            return collection.Exists(Query.EQ("_id", id));
        }

        public static T ToObject<T>(DataSet ds)
        {
            if (ds.ObjectInstance == null)
            {
                return default;
            }
            var key = typeof(T).FullName;
            T instance;
            if (ds.ObjectInstance.Exists(key))
            {
                instance = BsonMapper.Global.ToObject<T>(ds.ObjectInstance.FindById(key));
            }
            else
            {
                instance = Activator.CreateInstance<T>();
                ds.ObjectInstance.Insert(BsonMapper.Global.ToDocument<T>(instance));
            }

            return instance;
        }

        public static T ToObject<T>(DataSet ds, T instance)
        {
            if (ds.ObjectInstance == null)
            {
                return instance;
            }
            var key = typeof(T).FullName;
            if (!ds.ObjectInstance.Exists(key))
            {
                ds.ObjectInstance.Insert(BsonMapper.Global.ToDocument<T>(instance));
            }

            return instance;
        }
    }
}
