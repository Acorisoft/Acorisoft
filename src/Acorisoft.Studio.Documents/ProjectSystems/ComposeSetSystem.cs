using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.Documents.Resources;
using DryIoc;
using LiteDB;
using MediatR;
using Disposable = Acorisoft.Extensions.Platforms.Disposable;
using FileMode = System.IO.FileMode;
using Unit = System.Reactive.Unit;

namespace Acorisoft.Studio.ProjectSystems
{
    /// <summary>
    /// <see cref="ComposeSetSystem"/> 类型表示一个创作集系统接口，用于为应用程序提供创作集新建、打开、关闭等支持。
    /// </summary>
    public class ComposeSetSystem : Disposable, IComposeSetSystem
    {
        #region Fields

        protected readonly BehaviorSubject<bool> IsOpenStream;
        protected readonly BehaviorSubject<IComposeSet> ComposeSetStream;
        protected readonly BehaviorSubject<IComposeSetProperty> PropertyStream;
        protected readonly BehaviorSubject<Unit> EndOpeningStream;
        protected readonly BehaviorSubject<Unit> BeginOpeningStream;
        protected readonly IMediator MediatorInstance;
        protected readonly CompositeDisposable Disposable;
        
        private readonly ConcurrentQueue<Unit> _queue;

        protected IComposeSet CurrentComposite;
        protected bool IsOpenInstance;
        
        #endregion
        
        private ComposeSetSystem(IMediator mediator)
        {
            _queue = new ConcurrentQueue<Unit>();
            
            IsOpenStream = new BehaviorSubject<bool>(false);
            ComposeSetStream = new BehaviorSubject<IComposeSet>(null);
            PropertyStream = new BehaviorSubject<IComposeSetProperty>(null);
            EndOpeningStream = new BehaviorSubject<Unit>(Unit.Default);
            BeginOpeningStream = new BehaviorSubject<Unit>(Unit.Default);
            MediatorInstance = mediator ?? throw new ArgumentNullException(nameof(mediator));
            
            //
            // 初始化组合可释放收集器
            Disposable = new CompositeDisposable
            {
                IsOpenStream,
                ComposeSetStream,
                PropertyStream,
                EndOpeningStream,
                BeginOpeningStream
            };


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

                return instance;
            }
        }
        
        #region Methods


        protected override void OnDisposeManagedCore()
        {
            if (Disposable.IsDisposed)
            {
                return;
            }
            
            Disposable.Dispose();
            IsOpenInstance = false;
            
        }

        protected override void OnDisposeUnmanagedCore()
        {
            CurrentComposite?.Dispose();
            IsOpenInstance = false;
        }

        #region IComposeSetSystem Implementations

        private static string GetDatabaseFileNameFromPath(string path)
        {
            return Path.Combine(path, ProjectSystems.ComposeSet.MainDatabaseFileName);
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
                MainDatabase = GetDatabaseFromPath(GetDatabaseFileNameFromPath(project.Path)),
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
            IsOpenInstance = true;
            IsOpenStream.OnNext(IsOpenInstance);
            
            //
            // 更新属性
            var property = await GetPropertyAsync();
            PropertyStream.OnNext(property);

            await MediatorInstance.Publish(new ComposeSetCloseInstruction());
            await MediatorInstance.Publish(new ComposeSetOpenInstruction(composeSet));
        }

        /// <summary>
        /// 在一个异步请求中关闭一个项目。
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public async Task CloseAsync()
        {
            
            CurrentComposite?.Dispose();
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
            
            //
            // 创建 compose
            var compose = new ComposeSet(info.Path)
            {
                MainDatabase = GetDatabaseFromPath(GetDatabaseFileNameFromPath(info.Path)),
            };

            await OpenAsync(compose);
            await SetPropertyAsync(info.Item);
        }

        #endregion

        #region IComposeSetFileSystem Implementations

        private Stream Open(string fileName)
        {
            return new FileStream(fileName, FileMode.Open);
        }

        private void UploadImpl(string sourceFileName, string targetFileName)
        {
            if (!IsOpenInstance || string.IsNullOrEmpty(targetFileName))
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
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="isCache"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<Stream> OpenAsync(Resource resource, bool isCache)
        {
            if (!IsOpenInstance || resource == null) throw new InvalidOperationException("创作集未打开");
            try
            {
                return Task.Run(()=> Open(resource.GetResourceFileName(CurrentComposite)));
            }
            catch
            {
                // rethrow
                throw;
            }

            throw new InvalidOperationException("创作集未打开");
        }
        
        /// <summary>
        /// 在一个异步请求中完成获得指定资源的文件流操作。
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="isCache"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<Stream[]> OpenAsync(AlbumResource resource, bool isCache)
        {
            if (!IsOpenInstance || resource == null)
            {
                throw new InvalidOperationException("创作集未打开");
            }
            try
            {
                return Task.Run(() => resource.GetResourceFileNames(CurrentComposite).Select(Open).ToArray());
            }
            catch
            {
                // rethrow
                throw;
            }

            throw new InvalidOperationException("创作集未打开");
        }

        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceFileName">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(Resource resource, string sourceFileName)
        {
            return Task.Run(()=> UploadImpl(resource.GetResourceFileName(CurrentComposite), sourceFileName));
        }
        
        /// <summary>
        /// 在一个异步请求中完成文件上传操作。
        /// </summary>
        /// <param name="resource">指定要上传的资源类型。</param>
        /// <param name="sourceFileNames">指定上传操作的原始文件路径。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task UploadAsync(AlbumResource resource, IEnumerable<string> sourceFileNames)
        {
            return Task.Run(() =>
            {
                var fileNames = sourceFileNames.ToArray();
                var pathes = resource.GetResourceFileNames(CurrentComposite);
                var count = Math.Min(fileNames.Length, pathes.Length);
                for (var i = 0; i < count; i++)
                {
                    UploadImpl(fileNames[i], pathes[i]);
                }
            });
        }

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

        #region IComposeSetPropertySystem Implementations
        
        private void SetPropertyImpl(ComposeSetProperty property)
        {
            if (!IsOpenInstance || property == null)
            {
                return;
            }
            
            SetObject(property);
                
            //
            // 更新属性
            PropertyStream.OnNext(property);
        }
        
        private IComposeSetProperty GetPropertyImpl()
        {
            if (!IsOpenInstance)
            {
                throw new InvalidOperationException("无法获取属性，因为创作集未打开。");
            }
            
            var property = GetObject<ComposeSetProperty>();
                
            //
            // 更新属性
            PropertyStream.OnNext(property);

            return property;

        }


        protected TObject GetObject<TObject>()
        {
            // ReSharper disable once InvertIf
            if (IsOpenInstance)
            {
                var key = typeof(TObject).FullName;
                var database = ((IComposeSetDatabase) CurrentComposite).MainDatabase;
                var collection = database.GetCollection(Acorisoft.Studio.ProjectSystems.ComposeSet.MetadataCollection);
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
            var collection = database.GetCollection(Acorisoft.Studio.ProjectSystems.ComposeSet.MetadataCollection);
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
                BeginOpeningStream.OnNext(Unit.Default);
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
                EndOpeningStream.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 清空所有请求。
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
            EndOpeningStream.OnNext(Unit.Default);
        }

        #endregion

        #endregion

        #region Properties

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
        public IMediator Mediator => MediatorInstance;

        #endregion

        #region IComposeSetPropertySystem Implementations

        /// <summary>
        /// 获取当前创作集属性系统的一个数据流，当前数据流用于表示创作集属性系统中新的属性更新通知。
        /// </summary>
        public IObservable<IComposeSetProperty> Property => PropertyStream;

        #endregion

        #region IComposeSetRequestQueue Implementations

        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在打开。
        /// </summary>
        public IObservable<Unit> EndOpening => EndOpeningStream;

        /// <summary>
        /// 获取当前创作集请求队列的一个数据流，当前数据流用于表示创作集请求队列是否正在关闭。
        /// </summary>
        public IObservable<Unit> BeginOpening => BeginOpeningStream;

        #endregion

        #endregion
    }
}