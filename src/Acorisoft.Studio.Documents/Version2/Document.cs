using System;
using LiteDB;

namespace Acorisoft.Studio
{
    /// <summary>
    /// <see cref="Document"/> 类型表示一个文档
    /// </summary>
    public abstract class Document
    {
        [BsonId]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 获取文档名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastAccessTimestamp { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTimestamp { get; set; }
    }
}