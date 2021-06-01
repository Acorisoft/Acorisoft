using System;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public class DocumentIndexVersion1
    {
        [BsonId]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}