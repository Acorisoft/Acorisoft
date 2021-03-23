using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSet : Disposable, IDataSet, IDataSetImpl
    {
        /// <summary>
        /// 获取或设置应用于当前数据集的数据库对象。
        /// </summary>
        protected internal LiteDatabase Database { get; internal set; }

        /// <summary>
        /// 获取或设置应用于当前数据集的数据库对象。
        /// </summary>
        LiteDatabase IDataSetImpl.Database => Database;
    }
}
