using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public abstract class DataSet : Disposable, IDataSet
    {
        internal LiteDatabase DatabaseInstance;
        internal LiteCollection<BsonDocument> ObjectInstance;

        protected override void OnReleaseUnmanageCore()
        {
            DatabaseInstance?.Dispose();
        }

        public LiteDatabase Database => DatabaseInstance;
    }
}
