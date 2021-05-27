using System;
using System.IO;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.Engines;
using NLog.Internal;

namespace Acorisoft.Studio.ProjectSystem
{
    /// <summary>
    /// <see cref="CompositionSetFileManager"/> 类型用于表示一个创作集文件管理器，用于为应用程序提供统一的文件访问、文件存储功能。
    /// </summary>
    public class CompositionSetFileManager : ProjectSystemModule, ICompositionSetFileManager
    {
        private ICompositionSet _composition;
        public const string ImageScheme = "morisa-image";
        public const string VideoScheme = "morisa-video";
        public const string FileScheme = "morisa-file";
        
        public CompositionSetFileManager(ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
        }

        #region Override Methods

        
        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
            _composition = notification.CompositionSet;
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
            _composition = null;
        }

        protected override void OnCompositionSetSaving(CompositionSetSaveNotification notification)
        {
            
        }

        #endregion

        #region GetPathFromCompositionSet
        
        private string GetImageFromCompositionSet(string fileName)
        {
            return Path.Combine(_composition.GetCompositionSetImagesDirectory(), fileName);
        }
        
        private string GetVideoFromCompositionSet(string fileName)
        {
            return Path.Combine(_composition.GetCompositionSetVideosDirectory(), fileName);
        }
        
        private string GetFileFromCompositionSet(string fileName)
        {
            return Path.Combine(_composition.GetCompositionSetFilesDirectory(), fileName);
        }
        
        #endregion

        public async Task<Uri> UploadVideo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new InvalidOperationException("无法上传不存在的文件");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"Morisa-Video://{id}");
            
            //
            // 设置文件名
            var targetVideoName = GetVideoFromCompositionSet(id);
            
            //
            // 复制到指定目录
            await Task.Run(() => File.Copy(fileName, targetVideoName));
            
            return uri;
        }
        public async Task<Uri> UploadVideo(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("无法上传无效的流");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"Morisa-Video://{id}");
            
            //
            // 设置文件名
            var targetVideoName = GetVideoFromCompositionSet(id);
            
            //
            // 复制到指定目录
            using (var newVideoStream = new FileStream(targetVideoName, FileMode.Create))
            {
                await stream.CopyToAsync(newVideoStream);
            }
            
            return uri;
        }
        public async Task<Uri> UploadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new InvalidOperationException("无法上传不存在的文件");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"Morisa-File://{id}");
            
            //
            // 设置文件名
            var targetFileName = GetFileFromCompositionSet(id);
            
            //
            // 复制到指定目录
            await Task.Run(() => File.Copy(fileName, targetFileName));
            
            return uri;
        }
        public async Task<Uri> UploadFile(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("无法上传无效的流");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"Morisa-File://{id}");
            
            //
            // 设置文件名
            var targetFileName = GetFileFromCompositionSet(id);
            
            //
            // 复制到指定目录
            using (var newFileStream = new FileStream(targetFileName, FileMode.Create))
            {
                await stream.CopyToAsync(newFileStream);
            }
            
            return uri;
        }
        public async Task<Uri> UploadImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new InvalidOperationException("无法上传不存在的文件");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"{ImageScheme}://{id}");
            
            //
            // 设置文件名
            var targetFileName = GetImageFromCompositionSet(id);
            
            //
            // 复制到指定目录
            await Task.Run(() => File.Copy(fileName, targetFileName));
            
            return uri;
        }
        public async Task<Uri> UploadImage(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("无法上传无效的流");
            }
            
            //
            // 创建 ID
            var id = Guid.NewGuid().ToString("N");
            
            //
            // 创建URI
            var uri = new Uri($"{ImageScheme}://{id}");
            
            //
            // 设置文件名
            var targetFileName = GetImageFromCompositionSet(id);
            
            //
            // 复制到指定目录
            using (var newFileStream = new FileStream(targetFileName, FileMode.Create))
            {
                await stream.CopyToAsync(newFileStream);
            }
            
            return uri;
        }

        public Stream GetStream(Uri uri)
        {
            if (uri == null)
            {
                return null;
            }

            var scheme = uri.Scheme;
            var id = uri.DnsSafeHost;
            
            if (string.Compare(scheme,ImageScheme, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return GetImageStream(id);
            }
            else if (string.Compare(scheme,ImageScheme, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return GetImageStream(id);
            }
            
            return null;
        }

        private Stream GetImageStream(string id)
        {
            return new FileStream(GetImageFromCompositionSet(id), FileMode.Open);
        }
    }
}