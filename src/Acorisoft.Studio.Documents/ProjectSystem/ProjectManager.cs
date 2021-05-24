using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Platforms.ComponentModel;
using Acorisoft.Studio.Documents.Engines;
using DynamicData;
using LiteDB;
using MediatR;
using Unit = System.Reactive.Unit;
using LiteDBFileMode = LiteDB.FileMode;
using System.Threading.Tasks;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class ProjectManager : ObservableObject, IProjectManager
    {
        public const string MorisaDocumentDatabaseVersion1Suffix = ".Md2v1";
        public const string MorisaDocumentDatabaseVersion2Suffix = ".Md2v2";
        public const string MorisaDocumentDatabaseVersion3Suffix = ".Md2v3";
        public const long MorisaDocumentDatabaseInitSize = 32 * 1024 * 1024;
        public const int MorisaDocumentDatabaseCacheSize = 32 * 1024 * 1024;

        private protected readonly SourceList<CompositionProject> Source;


        #region IDocumentEngineAquirement

        private class RequestQueue : IDocumentEngineAquirement
        {
            private readonly Queue<Unit> _queue;
            private readonly Subject<Unit> _openStarting;
            private readonly Subject<Unit> _openEnding;

            public RequestQueue()
            {
                _queue = new Queue<Unit>();
                _openEnding = new Subject<Unit>();
                _openStarting = new Subject<Unit>();
            }

            public void Set()
            {
                _queue.Enqueue(Unit.Default);
                _openStarting.OnNext(Unit.Default);
            }

            public void Unset()
            {
                if (_queue.Count > 0)
                {
                    _queue.Dequeue();
                }

                if (_queue.Count == 0)
                {
                    //
                    // Raise Notification
                    _openEnding.OnNext(Unit.Default);
                }
            }

            public IObservable<Unit> ProjectOpenStarting => _openStarting;
            public IObservable<Unit> ProjectOpenEnding => _openEnding;
        }

        #endregion

        //
        // Project
        // <Root>
        // <Root>\Images
        // <Root>\Videos
        // <Root>\Brushes
        // <Root>\Maps
        // <Root>\Images

        private readonly RequestQueue _queue;
        private readonly IMediator _mediator;

        public ProjectManager(IMediator mediator)
        {
            //
            // 这是从数据库中加载项目
            Source = new SourceList<CompositionProject>();
            // Source.Connect()
            //     .Filter(x => x != null)
            //     .Page();
            _queue = new RequestQueue();
            _mediator = mediator;
        }

        //
        // ProjectManager 
        internal IDocumentEngineAquirement Aquirement => _queue;

        public void MockupOpen()
        {
            _mediator.Publish(new DocumentSwitchNotification());
        }

        internal static LiteDatabase GetProjectDatabase(string path)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = Path.Combine(path, MorisaDocumentDatabaseVersion1Suffix),
                InitialSize = MorisaDocumentDatabaseInitSize,
                Mode = LiteDBFileMode.Exclusive,
                CacheSize = MorisaDocumentDatabaseCacheSize,
            });
        }

        /// <summary>
        /// 创建一个新的设定集项目。
        /// </summary>
        /// <param name="newProjectInfo">传递一个 <see cref="INewProjectInfo"/> 类型实例作为参数。该参数要求不能为空</param>
        /// <exception cref="ArgumentNullException">当传递的参数为空时抛出该异常。</exception>
        /// <exception cref="InvalidOperationException">当传递的参数中</exception>
        public static ICompositionProject NewProject(INewProjectInfo newProjectInfo)
        {
            if (newProjectInfo == null)
            {
                throw new ArgumentNullException(nameof(newProjectInfo));
            }

            if (!string.IsNullOrEmpty(newProjectInfo.Name))
            {
                throw new InvalidOperationException("项目名为空");
            }

            if (!string.IsNullOrEmpty(newProjectInfo.Path))
            {
                throw new InvalidOperationException("路径为空");
            }

            if (Directory.Exists(newProjectInfo.Path))
            {
                throw new InvalidOperationException("路径错误");
            }

            try
            {
                //
                //
                return new CompositionProject
                {
                    Path = newProjectInfo.Path,
                    Name = newProjectInfo.Name,
                    Database = GetProjectDatabase(newProjectInfo.Path)
                };
            }
            catch
            {
                throw new InvalidOperationException("创建项目时失败");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Task OpenProject(ICompositionProject project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (!string.IsNullOrEmpty(project.Name))
            {
                throw new InvalidOperationException("项目名为空");
            }

            if (!string.IsNullOrEmpty(project.Path))
            {
                throw new InvalidOperationException("路径为空");
            }

            if (Directory.Exists(project.Path))
            {
                throw new InvalidOperationException("路径错误");
            }

            //
            // 
            try
            {
                //
                //
                CompositionProject compositionProject = project as CompositionProject ?? null;

                //
                // 打开一个项目或者创建新的项目
                var database = compositionProject.Database ?? GetProjectDatabase(project.Path);

                //
                // 推送通知
                var notification = new DocumentSwitchNotification
                {
                    Database = database
                };

                return _mediator.Publish(notification);
            }
            catch
            {
                throw new InvalidOperationException(nameof(project));
            }

        }

        public static string GetDatabaseFromProjectInformation(INewProjectInfo info)
        {
            return Path.Combine(info.Path, MorisaDocumentDatabaseVersion1Suffix);
        }

        public void Dispose()
        {
        }
    }
}