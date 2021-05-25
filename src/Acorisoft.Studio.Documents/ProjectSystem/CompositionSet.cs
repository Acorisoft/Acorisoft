using System;
using LiteDB;
using System.IO;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSet : ICompositionSet, ICompositionSetDatabase, IDisposable
    {
        public const string ImagesDirectory = "Images";
        public const string VideosDirectory = "Videos";
        public const string BrushesDirectory = "Brushes";
        public const string MapsDirectory = "Maps";
        
        public string GetCompositionSetImagesDirectory()
        {
            return System.IO.Path.Combine(Path, ImagesDirectory);
        }
        
        public string GetCompositionSetVideosDirectory()
        {
            return System.IO.Path.Combine(Path, VideosDirectory);
        }
        
        public string GetCompositionSetBrushesDirectory()
        {
            return System.IO.Path.Combine(Path, BrushesDirectory);
        }
        
        public string GetCompositionSetMapsDirectory()
        {
            return System.IO.Path.Combine(Path, MapsDirectory);
        }
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