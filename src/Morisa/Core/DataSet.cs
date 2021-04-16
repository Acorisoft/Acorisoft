using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="DataSet"/>
    /// </summary>
    public abstract class DataSet : Disposable, IDataSet
    {
        internal LiteDatabase DatabaseInstance;
        internal LiteCollection<BsonDocument> ObjectInstance;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnReleaseUnmanageCore()
        {
            DatabaseInstance?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public LiteDatabase Database => DatabaseInstance;
    }
}
