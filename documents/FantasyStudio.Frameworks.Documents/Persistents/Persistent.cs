using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Persistents
{
    public abstract class Persistent<TObject, TInterface> : IDataPersistent where TObject : class, TInterface where TInterface : notnull
    {
        public void Initialize()
        {
            BsonMapper.Global.RegisterType<TObject>(Serialize, Deserialize);
            BsonMapper.Global.RegisterType<TInterface>(Serialize, Deserialize);
        }

        public virtual BsonDocument Serialize(TInterface instance)
        {
            return BsonMapper.Global.ToDocument(instance);
        }

        public virtual BsonDocument Serialize(TObject instance)
        {
            return BsonMapper.Global.ToDocument(instance);
        }

        public virtual TObject Deserialize(BsonValue value)
        {
            return BsonMapper.Global.ToObject<TObject>(value.AsDocument);
        }
    }
}
