using System;
using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public abstract class Document
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}