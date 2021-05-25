using System;
using System.IO;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents.Engines;
using NLog.Internal;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    /// <summary>
    /// <see cref="CompositionSetFileManager"/> 类型用于表示一个创作集文件管理器，用于为应用程序提供统一的文件访问、文件存储功能。
    /// </summary>
    public class CompositionSetFileManager : ProjectSystemHandler, ICompositionSetFileManager
    {
        private ICompositionSet _composition;
        
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

        #endregion

        private string GetImageFromCompositionSet(string fileName)
        {
            return Path.Combine(_composition.GetCompositionSetImagesDirectory(), fileName);
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
            var uri = new Uri($"Morisa://acorisoft.tech/Images/{id}", UriKind.Relative);
            
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
            var uri = new Uri($"Morisa://acorisoft.tech/Images/{id}", UriKind.Relative);
            
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
    }
}