using System;
using System.IO;
using System.Threading.Tasks;
using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="IComposeSetFileSystem"/> 接口表示一个抽象的创作集文件系统接口，用于实现创作集文件访问和修改的支持。
    /// </summary>
    public interface IComposeSetFileSystem
    {
        /// <summary>
        /// 验证资源访问是否成功
        /// </summary>
        /// <param name="resource">要访问的资源。</param>
        /// <returns>返回一个值，该值表示当前资源访问是否成功。如果成功则返回true,否则返回false。</returns>
        bool VerifyResourceAccess(Resource resource);
        
        /// <summary>
        /// 获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource">指定要获取文件流的资源。</param>
        /// <exception cref="FileNotFoundException">文件不存在</exception>
        /// <exception cref="UnauthorizedAccessException">没有访问权限</exception>
        /// <exception cref="IOException">其他文件异常。</exception>
        /// <returns>返回获得的文件流。如果文件不存在则会引发异常。</returns>
        Stream Open(Resource resource);
        
        /// <summary>
        /// 获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource">指定要获取文件流的资源。</param>
        /// <exception cref="FileNotFoundException">文件不存在</exception>
        /// <exception cref="UnauthorizedAccessException">没有访问权限</exception>
        /// <exception cref="IOException">其他文件异常。</exception>
        /// <returns>返回获得的文件流。如果文件不存在则会引发异常。</returns>
        Stream[] Open(AlbumResource resource);

        /// <summary>
        /// 获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource">指定要获取文件流的资源。</param>
        /// <exception cref="FileNotFoundException">文件不存在</exception>
        /// <exception cref="UnauthorizedAccessException">没有访问权限</exception>
        /// <exception cref="IOException">其他文件异常。</exception>
        /// <returns>返回获得的文件流。如果文件不存在则会引发异常。</returns>
        Task<Stream[]> OpenAsync(AlbumResource resource);
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task<Stream> OpenAsync(Resource resource);
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceFileName">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UploadAsync(Resource resource, string sourceFileName);
        
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceStream">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UploadAsync(Resource resource, Stream sourceStream);

        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="sourceResource">指定要上传的资源类型。</param>
        /// <param name="targetResource">指定要合并的资源。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UploadAsync(AlbumResource sourceResource, CoverResource targetResource);

        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="sourceResource">指定要上传的资源类型。</param>
        /// <param name="targetResource">指定要合并的资源。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UploadAsync(AlbumResource sourceResource, AlbumResource targetResource);
        
        /// <summary>
        /// 在一个异步请求中完成文件下载操作。
        /// </summary>
        /// <param name="resource">指定要下载的资源类型。</param>
        /// <param name="sourceFileName">指定下载操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task DownloadAsync(Resource resource, string sourceFileName);
    }
}