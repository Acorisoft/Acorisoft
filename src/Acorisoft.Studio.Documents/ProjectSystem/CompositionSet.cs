using System;
using LiteDB;
using System.IO;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSet : ICompositionSet, ICompositionSetDatabase, IDisposable,IEquatable<CompositionSet>
    {
        public const string ImagesDirectory = "Images";
        public const string VideosDirectory = "Videos";
        public const string BrushesDirectory = "Brushes";
        public const string MapsDirectory = "Maps";
        public const string FilesDirectory = "Files";

        public CompositionSet(string name,string path)
        {
            Path = path;
            Name = name;
        }

        public bool Equals(CompositionSet? y)
        {
            if (y == null)
            {
                return false;
            }

            return y.Name == Name && y.Path == Path;
        }

        public override bool Equals(object? obj)
        {
            if (obj is CompositionSet y)
            {
                return Equals(y);
            }

            return ReferenceEquals(obj, this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Path);
        }

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
        public string GetCompositionSetFilesDirectory()
        {
            return System.IO.Path.Combine(Path, FilesDirectory);
        }
        
        public string Name { get; }
        public string Path { get; }
        public LiteDatabase MainDatabase { get; set; }
        public ICompositionSetProperty Property { get; set; }
        public void Dispose()
        {
            MainDatabase?.Dispose();
        }
    }
}