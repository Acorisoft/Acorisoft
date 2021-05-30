using System.IO;
using Acorisoft.Extensions.Platforms;
using LiteDB;

namespace Acorisoft.Studio.ProjectSystems
{
    public class ComposeSet : Disposable, IComposeSet, IComposeSetDatabase
    {
        internal const string MetadataCollection = "Metadata";
        internal const string ImageFolder = "Images";
        internal const string VideoFolder = "Video";
        internal const string FileFolder = "Files";
        internal const string BrushFolder = "Brushes";
        internal const string CacheFolder = "Caches";
        internal const string GitFolder = ".git";
        internal const string DatabaseVersion1Suffix = ".Md2v1";
        internal const string MainDatabaseName = "Main";
        internal const string MainDatabaseFileName = MainDatabaseName + DatabaseVersion1Suffix;
        internal const int MainDatabaseSize = 32 * 1024 * 1024;
        internal const int MainDatabaseCacheSize = 32 * 1024 * 1024;

        private readonly string _path;

        public ComposeSet(string path)
        {
            _path = path;
        }
        
        public string GetComposeSetPath() => _path;

        public string GetComposeSetPath(ComposeSetKnownFolder folder)
        {
            return folder switch
            {
                ComposeSetKnownFolder.Brush => Path.Combine(_path, BrushFolder),
                ComposeSetKnownFolder.Cache => Path.Combine(_path, CacheFolder),
                ComposeSetKnownFolder.File => Path.Combine(_path, FileFolder),
                ComposeSetKnownFolder.Git => Path.Combine(_path, GitFolder),
                ComposeSetKnownFolder.Video => Path.Combine(_path, VideoFolder),
                _ => Path.Combine(_path, ImageFolder),
            };
        }

        public LiteDatabase MainDatabase { get; internal set; }
    }
}