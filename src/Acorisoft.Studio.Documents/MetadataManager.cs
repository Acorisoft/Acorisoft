using System;
using DryIoc;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public abstract class MetadataManager : IMetadataManager
    {
        private LiteDatabase _database;
        private LiteCollection<BsonDocument> _collection;

        public const string MetadataCollectionName = "Metadatas";
        
        public T GetObject<T>()
        {
            //
            // Get the instance key from type qualified name
            var instanceKey = typeof(T).FullName;
            T instance;
            
            if (_collection.Exists(instanceKey))
            {
                instance = BsonMapper.Global.ToObject<T>(_collection.FindById(instanceKey).AsDocument);
            }
            else
            {
                instance = Activator.CreateInstance<T>();
                _collection.Insert(instanceKey, BsonMapper.Global.ToDocument(instance));
            }

            return instance;
        }

        public T SetObject<T>(T instance)
        {
            instance ??= Activator.CreateInstance<T>();
            var instanceKey = instance.GetType().FullName;
            _collection.Upsert(instanceKey, BsonMapper.Global.ToDocument(instance));
            return instance;
        }

        public T SetObject<T>(Func<T> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            var instance = factory.Invoke() ?? Activator.CreateInstance<T>();
            var instanceKey = instance.GetType().FullName;
            _collection.Upsert(instanceKey, BsonMapper.Global.ToDocument(instance));
            return instance;
        }

        public object SetObject(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            
            var instanceKey = instance.GetType().FullName;
            _collection.Upsert(instanceKey, BsonMapper.Global.ToDocument(instance));
            return instance;
        }

        public void SetDatabase(LiteDatabase database)
        {
            //
            // force previous field to null
            _database = null;
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _collection = _database.GetCollection(MetadataCollectionName);
        }
    }
}