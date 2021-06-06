using System;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.ProjectSystems
{
    /// <summary>
    /// <see cref="IComposeSetFileSystem2"/> 接口用于表示一个抽象的创作集文件系统接口，它是基础文件系统 <see cref="IComposeSetFileSystem"/> 的升级版。
    /// </summary>
    public interface IComposeSetFileSystem2 : IComposeSetFileSystem
    {
        #region Synchronize Methods
        
        /// <summary>
        /// 获取一个值，该值表示指定的资源是否存在。
        /// </summary>
        /// <param name="resource">指定要判断的资源。</param>
        /// <returns>返回一个值，该值表示指定的资源是否存在。true表示资源存在，否则表示不存在。</returns>
        bool ResourceExists(Resource resource);

        /// <summary>
        /// 打开一个资源，并返回该资源的数据流。
        /// </summary>
        /// <param name="resource">指定要打开的资源</param>
        /// <returns>返回一个数据流，该数据流用于实现对资源的读取。</returns>
        Stream OpenResource(Resource resource);

        /// <summary>
        /// 打开一个资源，并返回该资源的数据流。
        /// </summary>
        /// <param name="resource">指定要打开的资源</param>
        /// <returns>返回多个数据流，该数据流用于实现对资源的读取。</returns>
        Stream[] OpenResources(Resource resource);

        /// <summary>
        /// 将指定的文件与指定的资源关联，并上传到数据库。
        /// </summary>
        /// <param name="resource">指定要关联的资源。</param>
        /// <param name="fileName">指定要上传的文件</param>
        void UploadResource(Resource resource, string fileName);
        
        /// <summary>
        /// 将指定的数据流与指定的资源关联，并上传到数据库。
        /// </summary>
        /// <param name="resource">指定要关联的资源。</param>
        /// <param name="stream">指定要上传的数据流</param>
        void UploadResource(Resource resource, Stream stream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        void AutoSave(Resource resource);
        #endregion
        
        
        #region Asynchronize Methods
        
        /// <summary>
        /// 在一个异步操作中判断指定的资源是否存在。
        /// </summary>
        /// <param name="resource">指定要判断的资源。</param>
        /// <returns>返回一个可等待的 <see cref="Task"/> 任务实例，等待该任务将会返回一个值，该值表示指定的资源是否存在。true表示资源存在，否则表示不存在。</returns>
        Task<bool> ResourceExistsAsync(Resource resource);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        Task<Stream> OpenResourceAsync(Resource resource);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        Task<Stream[]> OpenResourcesAsync(Resource resource);
        
        /// <summary>
        /// 将指定的文件与指定的资源关联，并上传到数据库。
        /// </summary>
        /// <param name="resource">指定要关联的资源。</param>
        /// <param name="fileName">指定要上传的文件</param>
        Task UploadResourceAsync(Resource resource, string fileName);
        
        /// <summary>
        /// 将指定的数据流与指定的资源关联，并上传到数据库。
        /// </summary>
        /// <param name="resource">指定要关联的资源。</param>
        /// <param name="stream">指定要上传的数据流</param>
        Task UploadResourceAsync(Resource resource, Stream stream);
        
        
        
        
        
        
        
        
        #endregion
        
        /// <summary>
        /// 用于提示存储冲突的消息流。
        /// </summary>
        IObservable<Unit> DuplicateNotification { get; }
        
        /// <summary>
        /// 用于接收存储冲突时的操作。
        /// </summary>
        IObserver<bool> DuplicateOperation { get; } 
    }
}