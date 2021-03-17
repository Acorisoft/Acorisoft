using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalCollection = LiteDB.LiteCollection<LiteDB.BsonDocument>;

namespace Acorisoft.Morisa
{
    public abstract class DataSet
    {        
        /// <summary>
        /// 提供给工厂访问的数据库。
        /// </summary>
        internal LiteDatabase Database;

        /// <summary>
        /// 提供给工厂访问的额外数据集。
        /// </summary>
        internal ExternalCollection DB_External;
    }
}
