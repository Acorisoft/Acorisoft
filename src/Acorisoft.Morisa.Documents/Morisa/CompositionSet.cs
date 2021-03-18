using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    partial class CompositionSet : DataSet<CompositionSetInformation>
    {
        public const string ImageDirectoryName = "Images";
        public const string VideoDirectoryName = "Videos";
        public const string FontsDirectoryName = "Fonts";
        public const string BrushesDirectoryName = "Brushes";

        internal protected static string EnsureDirectory(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            return directory;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get; internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Directory
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, ImageDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string VideoDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, VideoDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FontsDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, FontsDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BrushesDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, BrushesDirectory));
            }
        }
    }
}
