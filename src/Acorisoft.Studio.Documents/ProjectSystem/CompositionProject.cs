using System;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionProject : ICompositionProject
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}