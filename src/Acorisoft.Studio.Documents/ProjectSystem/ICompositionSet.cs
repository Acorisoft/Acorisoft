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
        public string GetCompositionSetFilesDirectory();
        string Name { get;  }
        string Path { get;  }
        Guid Id { get; }
        ICompositionSetProperty Property { get; set; }
    }

    interface ICompositionSetDatabase
    {
        LiteDatabase MainDatabase { get; }
    }
}