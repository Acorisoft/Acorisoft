using System;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public class DatabaseTransaction : IDisposable
    {
        private readonly LiteDatabase _database;
        public DatabaseTransaction(LiteDatabase database)
        {
            _database = database;
        }

        public void Start()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}