using System;
using System.IO;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents.Resources;

namespace Acorisoft.Studio.ProjectSystems
{
    /// <summary>
    /// <see cref="IComposeSetFileSystem"/> 接口表示一个抽象的创作集文件系统接口，用于实现创作集文件访问和修改的支持。
    /// </summary>
    public interface IComposeSetFileSystem : IDisposable
    {
        /// <summary>
        /// 获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        Stream Open(Resource resource);
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="callback"></param>
        void OpenAsync(Resource resource, TaskCallback callback);
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="isCache"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task<Stream> OpenAsync(Resource resource, bool isCache);
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceFileName">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UploadAsync(Resource resource, string sourceFileName);
        
        /// <summary>
        /// 在一个异步请求中完成文件下载操作。
        /// </summary>
        /// <param name="resource">指定要下载的资源类型。</param>
        /// <param name="sourceFileName">指定下载操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task DownloadAsync(Resource resource, string sourceFileName);
    }
}