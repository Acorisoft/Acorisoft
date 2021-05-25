using System;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public interface IMetadataManager
    {
        T GetObject<T>();
        T SetObject<T>(T instance);
        T SetObject<T>(Func<T> factory);
        object SetObject(object instance);
        void SetDatabase(LiteDatabase database);
    }
}