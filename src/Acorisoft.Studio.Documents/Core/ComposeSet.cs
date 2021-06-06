using System.IO;
using Acorisoft.Extensions.Platforms;
using LiteDB;

namespace Acorisoft.Studio.Systems
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
        internal const string ManifestContentName = "Manifest.json";

        private readonly string _path;

        public ComposeSet(string path)
        {
            _path = path;
        }

        public string GetComposeSetPath(ComposeSetKnownFolder folder)
        {
            return folder switch
            {
                ComposeSetKnownFolder.Brush => GetBrushFolder(),
                ComposeSetKnownFolder.Cache => GetCacheFolder(),
                ComposeSetKnownFolder.File => GetFileFolder(),
                ComposeSetKnownFolder.Git => GetGitFolder(),
                ComposeSetKnownFolder.Video => GetVideoFolder(),
                ComposeSetKnownFolder.AutoSave => GetAutoSaveFolder(),
                _ => GetImageFolder(),
            };
        }

        protected internal string GetBrushFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, BrushFolder));
        protected internal string GetCacheFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, CacheFolder));
        protected internal string GetFileFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, FileFolder));
        protected internal string GetGitFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, GitFolder));
        protected internal string GetImageFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, ImageFolder));
        protected internal string GetVideoFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, VideoFolder));
        protected internal string GetAutoSaveFolder() => GetDirectoryAndCreate(System.IO.Path.Combine(_path, BrushFolder));



        public void MaintainDirectory()
        {
            GetBrushFolder();
            GetCacheFolder();
            GetFileFolder();
            GetGitFolder();
            GetImageFolder();
            GetVideoFolder();
            GetAutoSaveFolder();
        }

        private static string GetDirectoryAndCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// 获取当前创作集的路径。
        /// </summary>
        public string Path => _path;
        
        /// <summary>
        /// 获取当前创作集的属性。
        /// </summary>
        public IComposeSetProperty Property { get; set; }
        
        /// <summary>
        /// 获取当前创作集的主数据库。
        /// </summary>
        public LiteDatabase MainDatabase { get; internal set; }

        /// <summary>
        /// 获取清单列表。
        /// </summary>
        public string AutoSaveManifestFileName
        {
            get
            {
                return System.IO.Path.Combine(GetAutoSaveFolder(), ManifestContentName);
            }
        }
    }
}