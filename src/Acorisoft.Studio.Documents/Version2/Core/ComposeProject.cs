using System;
using Acorisoft.Studio.Resources;
using LiteDB;

namespace Acorisoft.Studio.Core
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