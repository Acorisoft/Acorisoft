using System;
using Acorisoft.Studio.Documents.Resources;
using LiteDB;

namespace Acorisoft.Studio.ProjectSystems
{
    public class ComposeProject : IComposeProject
    {
        [BsonId]
        public Guid Id { get; set; }
        public ImageResource Album { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}