using System;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public abstract class DocumentVersion1
    {
        [BsonId]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastAccessBy { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedBy { get; set; }
    }
}