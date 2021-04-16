using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Resources
{
    /// <summary>
    /// <see cref="DatabaseImageResource"/> 表示一个在数据库当中的图片资源。
    /// </summary>
    [DebuggerDisplay("{Id} - {FileName}")]
    public class DatabaseImageResource : IDatabaseImageResource
    {
        /// <summary>
        /// 获取或设置当前资源的唯一标识符。
        /// </summary>
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值用于表示当前资源是否已经插入数据库。
        /// </summary>
        [BsonIgnore]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 获取或设置当前资源的本地文件路径。
        /// </summary>
        [BsonIgnore]
        public string FileName { get; set; }

        public override string ToString()
        {
            return $"{Id:N}";
        }
    }
}
