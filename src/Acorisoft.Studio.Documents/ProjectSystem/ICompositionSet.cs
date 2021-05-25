using System;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface ICompositionSet : IDisposable
    {
        string GetCompositionSetImagesDirectory();

        public string GetCompositionSetVideosDirectory();

        public string GetCompositionSetBrushesDirectory();

        public string GetCompositionSetMapsDirectory();
        string Name { get; set; }
        string Path { get; set; }
        ICompositionSetProperty Property { get; set; }
    }

    interface ICompositionSetDatabase
    {
        LiteDatabase MainDatabase { get; }
    }
}