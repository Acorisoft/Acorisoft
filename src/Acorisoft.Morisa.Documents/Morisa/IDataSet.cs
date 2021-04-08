using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSet"/> 接口用于表示一个抽象的数据集。
    /// </summary>
    public interface IDataSet : IDisposable
    {
        /// <summary>
        /// 获取一个数据集存储单例对象的集合，该属性用于为使用者提供低级的对象存储操作支持。
        /// </summary>
        LiteCollection<BsonDocument> Objects { get; }

        /// <summary>
        /// 获取或设置应用于当前数据集的数据库对象，该属性用于为使用者提供低级的存储操作支持。
        /// </summary>
        LiteDatabase Database { get; }
    }
}
