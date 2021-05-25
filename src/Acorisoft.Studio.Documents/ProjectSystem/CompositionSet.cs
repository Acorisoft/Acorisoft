using System;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSet : ICompositionSet, ICompositionSetDatabase, IDisposable
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public LiteDatabase MainDatabase { get; set; }
        public ICompositionSetProperty Property { get; set; }
        public void Dispose()
        {
            MainDatabase?.Dispose();
        }
    }
}