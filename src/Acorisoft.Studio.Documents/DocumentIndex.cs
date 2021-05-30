using System;
using LiteDB;

namespace Acorisoft.Studio
{
    /// <summary>
    /// <see cref="DocumentIndex"/> 表示一个创作索引
    /// </summary>
    public class DocumentIndex
    {
        [BsonId]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
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