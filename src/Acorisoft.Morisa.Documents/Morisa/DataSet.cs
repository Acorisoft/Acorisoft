using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Core;
using LiteDB;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataSet"/> 表示一个数据集，用于为上层提供数据存储、读取支持。
    /// </summary>
    public abstract class DataSet : Disposable, IDataSet
    {
        internal LiteDatabase DatabaseInstance;
        internal LiteCollection<BsonDocument> ObjectsInstance;

        protected override sealed void OnDisposeUnmanagedCore()
        {
            DatabaseInstance?.Dispose();
        }

        protected override sealed void OnDisposeManagedCore()
        {
            ObjectsInstance = null;
        }

        /// <summary>
        /// 获取或设置应用于当前数据集的数据库对象，该属性用于为使用者提供低级的存储操作支持。
        /// </summary>
        public LiteCollection<BsonDocument> Objects => ObjectsInstance;

        /// <summary>
        /// 获取一个数据集存储单例对象的集合，该属性用于为使用者提供低级的对象存储操作支持。
        /// </summary>
        public LiteDatabase Database => DatabaseInstance;
    }
}
