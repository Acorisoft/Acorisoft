using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.Resources;
using Acorisoft.Studio.Engines;
using DryIoc;
using LiteDB;
using MediatR;
using Disposable = Acorisoft.Extensions.Platforms.Disposable;
using FileMode = System.IO.FileMode;
using Unit = System.Reactive.Unit;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="ComposeSetSystem"/> 类型表示一个创作集系统接口，用于为应用程序提供创作集新建、打开、关闭等支持。
    /// </summary>
    public class ComposeSetSystem : Disposable, IComposeSetSystem
    {
        #region Fields

        
        
        //-----------------------------------------------------------------------
        //
        //  Protected Readonly Fields
        //
        //-----------------------------------------------------------------------
        protected readonly BehaviorSubject<bool> IsOpenStream;
        protected readonly BehaviorSubject<IComposeSet> ComposeSetStream;
        protected readonly BehaviorSubject<IComposeSetProperty> PropertyStream;
        protected readonly BehaviorSubject<Unit> RespondingStream;
        protected readonly BehaviorSubject<Unit> RequestingStream;
        protected readonly IMediator MediatorField;
        protected readonly CompositeDisposable Disposable;
        
        
        
        //-----------------------------------------------------------------------
        //
        //  Private Readonly Fields
        //
        //-----------------------------------------------------------------------
        private readonly ConcurrentQueue<Unit> _queue;
        
        
        #endregion
        
        private ComposeSetSystem(IMediator mediator)
        {
            _queue = new ConcurrentQueue<Unit>();
            
            IsOpenStream = new BehaviorSubject<bool>(false);
            ComposeSetStream = new BehaviorSubject<IComposeSet>(null);
            PropertyStream = new BehaviorSubject<IComposeSetProperty>(null);
            RespondingStream = new BehaviorSubject<Unit>(Unit.Default);
            RequestingStream = new BehaviorSubject<Unit>(Unit.Default);
            MediatorField = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //
            // 初始化组合可释放收集器
            Disposable = new CompositeDisposable
            {
                IsOpenStream,
                ComposeSetStream,
                PropertyStream,
                RespondingStream,
                RequestingStream
            };


        }

        
        //-----------------------------------------------------------------------
        //
        //  Ioc Registraction
        //
        //-----------------------------------------------------------------------
        
        
        
        #region IocRegistraction

        

        private static void RegisterComposeSetSystemModule<TInstance>(IContainer container, TInstance instance) where TInstance : IComposeSetSystemModule
        {
            container.UseInstance<INotificationHandler<ComposeSetOpenInstruction>>(instance);
            container.UseInstance<INotificationHandler<ComposeSetSaveInstruction>>(instance);
            container.UseInstance<INotificationHandler<ComposeSetCloseInstruction>>(instance);
        }
        
        public static IComposeSetSystem Create(IContainer container)
        {
            if (container.IsRegistered<IComposeSetSystem>())
            {
                return container.Resolve<IComposeSetSystem>();
            }
            else
            {
                var factory = new ServiceFactory(container.Resolve);
                var mediator = new Mediator(factory);
                
                //
                // 注册中介者
                container.RegisterInstance<Mediator>(mediator);
                container.UseInstance<IMediator>(mediator);

                var instance = new ComposeSetSystem(mediator);
                container.RegisterInstance<ComposeSetSystem>(instance);
                container.UseInstance<IComposeSetSystem>(instance);
                container.UseInstance<IComposeSetFileSystem>(instance);
                container.UseInstance<IComposeSetRequestQueue>(instance);
                container.UseInstance<IComposeSetPropertySystem>(instance);

                var stickyNoteEngine = new StickyNoteEngine(instance);
                
                container.RegisterInstance<StickyNoteEngine>(stickyNoteEngine);
                container.UseInstance<IStickyNoteEngine>(stickyNoteEngine);
                RegisterComposeSetSystemModule(container, stickyNoteEngine);

                RegisterInspiration(container, instance);
                return instance;
            }
        }

        private static void RegisterInspiration(IContainer container, IComposeSetSystem instance)
        {
            
            var inspirationEngine = new InspirationEngine(instance);
                
            container.RegisterInstance<InspirationEngine>(inspirationEngine);
            container.UseInstance<IInspirationEngine>(inspirationEngine);
            RegisterComposeSetSystemModule(container, inspirationEngine);
        }
        
        
        #endregion
        
                
        //-----------------------------------------------------------------------
        //
        //  Methods
        //
        //-----------------------------------------------------------------------
        
        #region Methods

        
                
        //-----------------------------------------------------------------------
        //
        //  Protected Methods
        //
        //-----------------------------------------------------------------------
        
        
        #region Protected Methods

        protected override void OnDisposeManagedCore()
        {
            if (Disposable.IsDisposed)
            {
                return;
            }
            
            Disposable.Dispose();
            IsOpenField = false;
            
        }

        protected override void OnDisposeUnmanagedCore()
        {
            CurrentComposite?.Dispose();
            IsOpenField = false;
        }

        
        #endregion
        
        
        
        
        
        
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  IComposeSetSystem Implementations
        //
        //-----------------------------------------------------------------------
        #region IComposeSetSystem Implementations

        private static string GetDatabaseFileNameFromPath(string path)
        {
            return Path.Combine(path, Systems.ComposeSet.MainDatabaseFileName);
        }

        private static LiteDatabase GetDatabaseFromPath(string path)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = GetDatabaseFileNameFromPath(path),
                InitialSize = ProjectSystems.ComposeSet.MainDatabaseSize,
                CacheSize = ProjectSystems.ComposeSet.MainDatabaseCacheSize,
                Mode = LiteDB.FileMode.Exclusive
            });
        }
        
        /// <summary>
        /// 在一个异步请求中打开一个项目。
        /// </summary>
        /// <param name="project">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public async Task OpenAsync(IComposeProject project)
        {
            if (project == null)
            {
                return;
            }
            
            if (string.IsNullOrEmpty(project.Path))
            {
                throw new InvalidOperationException("path");
            }
            //
            // 创建 compose
            var compose = new ComposeSet(project.Path)
            {
                MainDatabase = GetDatabaseFromPath(project.Path)
            };

            await OpenAsync(compose);
        }
        
        
        /// <summary>
        /// 在一个异步请求中打开一个项目。
        /// </summary>
        /// <param name="composeSet">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public async Task OpenAsync(IComposeSet composeSet)
        {
            if (composeSet is null or IComposeSetDatabase {MainDatabase: null})
            {
                throw new ArgumentNullException(nameof(composeSet));
            }

            await CloseAsync();

            //
            // 设置创作集
            CurrentComposite = composeSet;
            ComposeSetStream.OnNext(composeSet);
            
            
            //
            // 设置打开状态
            IsOpenField = true;
            IsOpenStream.OnNext(IsOpenField);
            
            //
            // 初始化自动保存系统
            IsInitializeAutoSaveSystem = true;
            OpenAutoSaveManifest();
            
            //
            // 更新属性
            var property = await GetPropertyAsync();
            CurrentComposite.Property = property;
            PropertyStream.OnNext(property);

            await MediatorField.Publish(new ComposeSetCloseInstruction());
            await MediatorField.Publish(new ComposeSetOpenInstruction(composeSet));
        }

        /// <summary>
        /// 在一个异步请求中关闭一个项目。
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public async Task CloseAsync()
        {
            CurrentComposite?.Dispose();
            
            //
            // 关闭
            IsOpenField = false;
            IsOpenStream.OnNext(IsOpenField);
            
            //
            // 关闭自动保存系统
            IsInitializeAutoSaveSystem = false;
            CloseAutoSaveManifest();
            
            await Mediator.Publish(new ComposeSetCloseInstruction());
        }

        /// <summary>
        /// 在一个异步请求中新建一个项目。
        /// </summary>
        /// <param name="info">指定要打开的项目。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public async Task NewAsync(INewItemInfo<IComposeSetProperty> info)
        {
            if (info?.Item == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(info.Name))
            {
                info.Name = SR.ComposeSetSystem_EmptyName;
            }
            
            if (string.IsNullOrEmpty(info.Path))
            {
                throw new InvalidOperationException("path");
            }

            if (!Directory.Exists(info.Path))
            {
                Directory.CreateDirectory(info.Path);
            }

            if (File.Exists(GetDatabaseFileNameFromPath(info.Path)))
            {
                throw new InvalidOperationException("无法在已存在的项目文件夹中创建新的项目");
            }
            
            //
            // 创建 compose
            var compose = new ComposeSet(info.Path)
            {
                MainDatabase = GetDatabaseFromPath(info.Path),
            };

            await OpenAsync(compose);
            await SetPropertyAsync(info.Item);
        }

        #endregion
        
        
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  IComposeSetFileSystem Implementations
        //
        //-----------------------------------------------------------------------
        
        
        

        #region IComposeSetFileSystem Implementations

        
        //-----------------------------------------------------------------------
        //
        //  OpenAsync
        //
        //-----------------------------------------------------------------------
        private Stream OpenImpl(Resource resource)
        {
            return resource.Mode == ResourceMode.Inside ? OpenStreamFromDatabase(resource.GetResourceKey()) : OpenStreamFromOutside(resource.GetResourceFileName(CurrentComposite));
        }

        private Stream OpenStreamFromDatabase(string key)
        {
            var fs = ((IComposeSetDatabase) CurrentComposite).MainDatabase.FileStorage;
            return fs.OpenRead(key);
        }
        
        private static Stream OpenStreamFromOutside(string fileName)
        {
            return new FileStream(fileName, FileMode.Open);
        }

        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<Stream> OpenAsync(Resource resource)
        {
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }
            
            return Task.Run(()=> OpenImpl(resource));
        }

        private Stream[] OpenAlbumFromDatabase(AlbumResource resource)
        {
            return resource.GetResourceFileNames(CurrentComposite).Where(DetectInside).Select(OpenStreamFromDatabase).ToArray();
        }
        
        private Stream[] OpenAlbumFromOutside(AlbumResource resource)
        {
            return resource.GetResourceKeys().Where(DetectOutside).Select(OpenStreamFromDatabase).ToArray();
        }
        
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<Stream[]> OpenAsync(AlbumResource resource)
        {
            
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }

            return Task.Run(() => Open(resource));
        }
        
        public Stream[] Open(AlbumResource resource)
        {
            
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }

            return 
                resource.Mode == ResourceMode.Outside
                    ? OpenAlbumFromDatabase(resource)
                    : OpenAlbumFromOutside(resource);
        }
        
        
        public void OpenAsync(Resource resource, TaskCallback callback)
        {
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var stream = OpenImpl(resource);
            callback?.Invoke(stream);
        }
        
        public Stream Open(Resource resource)
        {
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }

            return OpenImpl(resource);
        }
        
        //-----------------------------------------------------------------------
        //
        //  VerifyResourceAccess
        //
        //-----------------------------------------------------------------------

        private bool DetectOutside(string fileName)
        {
            return File.Exists(fileName);
        }

        private bool DetectInside(string key)
        {
            var fs = ((IComposeSetDatabase) CurrentComposite).MainDatabase.FileStorage;
            return fs.Exists(key);
        }
        
        public Task<bool> VerifyResourceAccessAsync(Resource resource)
        {
            return Task.Run(() => VerifyResourceAccess(resource));
        }

        public bool VerifyResourceAccess(Resource resource)
        {
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (resource == null)
            {
                throw new InvalidOperationException("无法打开空的资源");
            }

            if (resource.GetType().IsAbstract)
            {
                throw new InvalidOperationException("无法打开抽象资源");
            }

            return resource.Mode == ResourceMode.Outside ? DetectOutside(resource.GetResourceFileName(CurrentComposite)) : DetectInside(resource.GetResourceKey());
        }


        //-----------------------------------------------------------------------
        //
        //  UploadAsync
        //
        //-----------------------------------------------------------------------

        private void UploadImpl(Resource resource, string sourceFileName)
        {
            if (resource.Mode == ResourceMode.Inside)
            {
                UploadFileToDatabase(sourceFileName, resource.GetResourceKey());
            }
            else
            {
                UploadFileToOutside(sourceFileName,resource.GetResourceFileName(CurrentComposite));
            }
        }
        
        private void UploadImpl(Resource resource, Stream sourceStream)
        {
            var targetFileName = resource.GetResourceFileName(CurrentComposite);
            
            if (resource.Mode == ResourceMode.Inside)
            {
                UploadStreamToDatabase(sourceStream, resource.GetResourceKey());
            }
            else
            {
                UploadStreamToOutside(sourceStream, resource.GetResourceFileName(CurrentComposite));
            }
        }
        
        private void UploadFileToOutside(string sourceFileName, string targetFileName)
        {
            if (!IsOpenField || string.IsNullOrEmpty(targetFileName))
            {
                throw new InvalidOperationException("创作集未打开");
            }
            
            try
            {
                File.Move(sourceFileName, targetFileName);
            }
            catch
            {
                // rethrow
                throw;
            }

            throw new InvalidOperationException("创作集未打开");
        }

        private void UploadFileToDatabase(string sourceFileName, string key)
        {
            var fs = ((IComposeSetDatabase) CurrentComposite).MainDatabase.FileStorage;
            fs.Upload(key, sourceFileName);
        }

        private void UploadStreamToOutside(Stream sourceStream, string targetFileName)
        {
            
            if (!IsOpenField)
            {
                throw new InvalidOperationException("创作集未打开");
            }

            if (string.IsNullOrEmpty(targetFileName))
            {
                throw new InvalidOperationException("无法打开空的资源");
            }
            
            try
            {
                using var targetStream = new FileStream(targetFileName, FileMode.Create);
                sourceStream.CopyTo(targetStream);
            }
            catch
            {
                // rethrow
                throw;
            }

        }
        private void UploadStreamToDatabase(Stream stream, string key)
        {
            
            var fs = ((IComposeSetDatabase) CurrentComposite).MainDatabase.FileStorage;
            fs.Upload(key, key, stream);
        }
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceFileName">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(Resource resource, string sourceFileName)
        {
            return Task.Run(()=> UploadImpl(resource, sourceFileName));
        }
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceStream">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(Resource resource, Stream sourceStream)
        {
            return Task.Run(()=> UploadImpl(resource, sourceStream));
        }
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="sourceResource">指定要上传的资源类型。</param>
        /// <param name="targetResource">指定要合并的资源。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(AlbumResource sourceResource, CoverResource targetResource)
        {
            if (sourceResource == null)
            {
                throw new ArgumentNullException(nameof(sourceResource));
            }

            if (targetResource == null)
            {
                throw new ArgumentNullException(nameof(targetResource));
            }
            
            return Task.Run(() =>
            {
                if (VerifyResourceAccess(targetResource))
                {
                    sourceResource.Add(targetResource.Id);
                }
            });
        }
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="sourceResource">指定要上传的资源类型。</param>
        /// <param name="targetResource">指定要合并的资源。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(AlbumResource sourceResource, AlbumResource targetResource)
        {
            if (sourceResource == null)
            {
                throw new ArgumentNullException(nameof(sourceResource));
            }

            if (targetResource == null)
            {
                throw new ArgumentNullException(nameof(targetResource));
            }
            
            return Task.Run(() =>
            {
                if (targetResource.Mode == ResourceMode.Inside)
                {
                    foreach (var key in targetResource)
                    {
                        if (DetectInside(key.ToString("N")))
                        {
                            sourceResource.Add(key);
                        }
                    }
                }
                else
                {
                    var iterator = targetResource.GetResourceFileNames(CurrentComposite).GetEnumerator();
                    foreach (var t in targetResource)
                    {
                        if (iterator.MoveNext() && DetectOutside(iterator.Current))
                        {
                            sourceResource.Add(t);
                        }
                    }
                }
            });
        }
        
        //-----------------------------------------------------------------------
        //
        //  DownloadAsync
        //
        //-----------------------------------------------------------------------

        /// <summary>
        /// 在一个异步请求中完成文件下载操作。
        /// </summary>
        /// <param name="resource">指定要下载的资源类型。</param>
        /// <param name="sourceFileName">指定下载操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task DownloadAsync(Resource resource, string sourceFileName)
        {
            return Task.Run(() => { });
        }

        #endregion
        
        
        
        //-----------------------------------------------------------------------
        //
        //  IComposeSetFileSystem2 Implementations
        //
        //-----------------------------------------------------------------------

        
        #region IComposeSetFileSystem2 Implementations


        /// <summary>
        /// 初始化自动保存系统。
        /// </summary>
        /// <remarks>
        /// 我建议在App.xaml.cs中使用，或者在 AppViewModel 中调用。
        /// </remarks>
        public void Initialize()
        {
            //
            // 该选项不能在
            if (!IsOpenField)
            {
                return;
            }

            if (!IsInitializeAutoSaveSystem)
            {
                return;
            }
        }

        private void OpenAutoSaveManifest()
        {
            
        }

        private void CloseAutoSaveManifest()
        {
            
        }

        #endregion
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  IComposeSetPropertySystem Implementations
        //
        //-----------------------------------------------------------------------

        #region IComposeSetPropertySystem Implementations
        
        private void SetPropertyImpl(ComposeSetProperty property)
        {
            if (!IsOpenField || property == null)
            {
                return;
            }
            
            SetObject(property);
                
            //
            // 更新属性
            
            CurrentComposite.Property = property;
            PropertyStream.OnNext(property);
        }
        
        private IComposeSetProperty GetPropertyImpl()
        {
            if (!IsOpenField)
            {
                throw new InvalidOperationException("无法获取属性，因为创作集未打开。");
            }
            
            var property = GetObject<ComposeSetProperty>();
                
            //
            // 更新属性
            CurrentComposite.Property = property;
            PropertyStream.OnNext(property);

            return property;

        }


        protected TObject GetObject<TObject>()
        {
            // ReSharper disable once InvertIf
            if (IsOpenField)
            {
                var key = typeof(TObject).FullName;
                var database = ((IComposeSetDatabase) CurrentComposite).MainDatabase;
                var collection = database.GetCollection(Acorisoft.Studio.Systems.ComposeSet.MetadataCollection);
                if (collection.Exists(key))
                {
                    return BsonMapper.Global.ToObject<TObject>(collection.FindById(key));
                }
                else
                {
                    return SetObject<TObject>(ClassActivator.CreateInstance<TObject>());
                }
            }

            throw new InvalidOperationException("无法获取属性，因为创作集未打开。");
            
        }
        
        protected TObject SetObject<TObject>(TObject instance)
        {
            var key = instance.GetType().FullName;
            var database = ((IComposeSetDatabase) CurrentComposite).MainDatabase;
            var collection = database.GetCollection(Acorisoft.Studio.Systems.ComposeSet.MetadataCollection);
            collection.Upsert(new BsonValue(key), BsonMapper.Global.ToDocument(instance));
            return instance;
        }
        
        /// <summary>
        /// 在一个异步请求中开启对创作集属性的修改行为。
        /// </summary>
        /// <param name="property">指定要修改的属性。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task SetPropertyAsync(IComposeSetProperty property)
        {
            return Task.Run(() => SetPropertyImpl(property as ComposeSetProperty));
        }

        /// <summary>
        /// 在一个异步请求中获取创作集属性。
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<IComposeSetProperty> GetPropertyAsync()
        {
            return Task.Run(GetPropertyImpl);
        }

        #endregion

        
        
        
        
        
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  IComposeSetRequestQueue Implementations
        //
        //-----------------------------------------------------------------------
        #region IComposeSetRequestQueue Implementations

        /// <summary>
        /// 设置一个请求。
        /// </summary>
        public void Set()
        {
            if (_queue.IsEmpty)
            {
                //
                // 推送通知。
                RequestingStream.OnNext(Unit.Default);
            }
            _queue.Enqueue(Unit.Default);
        }

        /// <summary>
        /// 取消一个请求
        /// </summary>
        public void Unset()
        {
            if (_queue.TryDequeue(out _) && _queue.IsEmpty)
            {
                RespondingStream.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 清空所有请求。
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
            RespondingStream.OnNext(Unit.Default);
        }

        #endregion
        
        
        
        
        
        
        
        
        

        #endregion
        
        
        //-----------------------------------------------------------------------
        //
        //  Properties
        //
        //-----------------------------------------------------------------------

        #region Properties
        
                
        //-----------------------------------------------------------------------
        //
        //  Protected Fields
        //
        //-----------------------------------------------------------------------
        protected IComposeSet CurrentComposite { get; private set; }
        
        protected bool IsOpenField { get; private set; }
        
        
        protected bool IsInitializeAutoSaveSystem { get; private set; }
        
        
        
        //-----------------------------------------------------------------------
        //
        //  Properties
        //
        //-----------------------------------------------------------------------

        #region IComposeSetSystem Implementations

        /// <summary>
        /// 获取当前创作集系统的一个数据流，当前数据流用于表示创作集系统是否已经打开。
        /// </summary>
        public IObservable<bool> IsOpen => IsOpenStream;

        /// <summary>
        /// 获取当前创作集系统的一个数据流，当前数据流用于表示创作集系统打开的创作集。
        /// </summary>
        public IObservable<IComposeSet> ComposeSet => ComposeSetStream;

        /// <summary>
        /// 获取当前创作集系统集成的中介者。
        /// </summary>
        public IMediator Mediator => MediatorField;

        #endregion
        
        
        
        
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  Properties
        //
        //-----------------------------------------------------------------------
        
        

        #region IComposeSetPropertySystem Implementations

        /// <summary>
        /// 获取当前创作集属性系统的一个数据流，当前数据流用于表示创作集属性系统中新的属性更新通知。
        /// </summary>
        public IObservable<IComposeSetProperty> Property => PropertyStream;

        #endregion
        
        
        
        
        
        
        
        
        
        
        
        //-----------------------------------------------------------------------
        //
        //  Properties
        //
        //-----------------------------------------------------------------------

        #region IComposeSetRequestQueue Implementations

        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在打开。
        /// </summary>
        public IObservable<Unit> Responding => RespondingStream;

        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在关闭。
        /// </summary>
        public IObservable<Unit> Requesting => RequestingStream;

        #endregion

        #endregion
    }
}