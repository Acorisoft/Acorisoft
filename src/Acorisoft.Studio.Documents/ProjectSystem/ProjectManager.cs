using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Disposables;
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

        public const string ArgumentNullProjectName = "ProjectName";
        public const string ArgumentNullPath = "Path";
        public const string InvalidOperationProjectExists = "Project Exists";
        public const string InvalidOperationCannotAddProject = "Cannot add project";
        public const string InvalidOperationDuplicateProject = "Cannot add project, project duplicate";
        public const string InvalidOperationAlreadyOpenProject = "Cannot open project, project already opened";

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

            public void Clear()
            {
                _queue.Clear();
                _openEnding.OnNext(Unit.Default);
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

        #region PropertyManager

        internal class CompositionSetPropertyManager : ICompositionSetPropertyManager
        {
            private ICompositionSetDatabase _composition;
            public void SetCompositionSet(ICompositionSet set)
            {
                _composition = set as ICompositionSetDatabase ?? throw new InvalidOperationException(nameof(set));
            }

            public void SetProperty(ICompositionSetProperty property)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private readonly HashSet<ICompositionSet> _projectDistinct;
        private protected readonly SourceList<ICompositionSet> Editable;
        private protected readonly ReadOnlyObservableCollection<ICompositionSet> Bindable;
        private protected readonly Subject<ICompositionSet> CurrentProjectStream;

        private readonly RequestQueue _queue;
        private readonly IMediator _mediator;
        private CompositionSet _current;

        public ProjectManager(IMediator mediator)
        {
            //
            // 创建基础字段
            _queue = new RequestQueue();
            _mediator = mediator;

            _projectDistinct = new HashSet<ICompositionSet>();

            Editable = new SourceList<ICompositionSet>();
            CurrentProjectStream = new Subject<ICompositionSet>();

            Editable.Connect().Bind(out Bindable).Subscribe();
        }

        private static LiteDatabase GetDatabaseFromPath(string path)
        {
            return new LiteDatabase(new ConnectionString
            {
              InitialSize  = MorisaDocumentDatabaseInitSize,
              CacheSize = MorisaDocumentDatabaseCacheSize,
              Filename = GetDatabaseConnectString(path),
              Mode = LiteDBFileMode.Exclusive
            });
        }
        
        private static string GetDatabaseConnectString(string path)
        {
            return Path.Combine(path,MorisaDocumentDatabaseVersion1Suffix);
        }

        public void Dispose()
        {
            foreach (var project in Editable.Items)
            {
                project.Dispose();
            }
            _queue.Clear();
            _current?.Dispose();
            Editable?.Dispose();
            
        }

        /// <summary>
        /// 创建项目并加载。
        /// </summary>
        public async Task NewProject(INewProjectInfo newProjectInfo)
        {
            if (newProjectInfo == null)
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_NewProject_ArgumentNull, nameof(newProjectInfo)));
            }

            if (string.IsNullOrEmpty(newProjectInfo.Name))
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_NewProject_ArgumentNull, ArgumentNullProjectName));
            }
            
            if (string.IsNullOrEmpty(newProjectInfo.Path))
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_NewProject_ArgumentNull, ArgumentNullPath));
            }

            if (!Directory.Exists(newProjectInfo.Path))
            {
                //
                // throw
                try
                {
                    Directory.CreateDirectory(newProjectInfo.Path);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format(SR.ProjectManager_NewProject_InvalidOperation, ex.Message), ex);
                } 
            }

            if (File.Exists(GetDatabaseConnectString(newProjectInfo.Path)))
            {
                throw new InvalidOperationException(string.Format(SR.ProjectManager_NewProject_InvalidOperation, InvalidOperationProjectExists));
            }
            
            //
            //
            try
            {
                //
                // 创建项目
                var compositionProject = new CompositionSet(newProjectInfo.Path)
                {
                    Name = newProjectInfo.Name,
                    MainDatabase = GetDatabaseFromPath(newProjectInfo.Path)
                };

                if (!_projectDistinct.Add(compositionProject))
                {
                    throw new InvalidOperationException(string.Format(SR.ProjectManager_NewProject_InvalidOperation, InvalidOperationCannotAddProject));
                }

                if (_current is not null)
                {
                    //
                    // 释放之前的内容
                    await _mediator.Publish(new DocumentCloseNotification());
                    _current.Dispose();
                }
                
                //
                // 添加到集合当中
                Editable.Add(compositionProject);
                
                //
                //
                _current = compositionProject;

                //
                // 推送更改
                await _mediator.Publish(new DocumentOpenNotification(compositionProject));
                
                //
                //
                CurrentProjectStream.OnNext(compositionProject);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(string.Format(SR.ProjectManager_NewProject_InvalidOperation, ex.Message), ex);
            } 
        }

        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="compositionProject"></param>
        /// <param name="isOpen"></param>
        public async Task LoadProject(CompositionProject compositionProject, bool isOpen = false)
        {
            if (compositionProject == null)
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_LoadProject_ArgumentNull, nameof(compositionProject)));
            }

            if (compositionProject.Equals(_current))
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, InvalidOperationAlreadyOpenProject));
            }
            
            if (string.IsNullOrEmpty(compositionProject.Name))
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, ArgumentNullProjectName));
            }
            
            if (string.IsNullOrEmpty(compositionProject.Path))
            {
                throw new ArgumentNullException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, ArgumentNullPath));
            }

            if (!Directory.Exists(compositionProject.Path))
            {
                //
                // throw
                try
                {
                    Directory.CreateDirectory(compositionProject.Path);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, ex.Message), ex);
                } 
            }

            if (!File.Exists(GetDatabaseConnectString(compositionProject.Path)))
            {
                throw new InvalidOperationException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, InvalidOperationProjectExists));
            }

            if (_projectDistinct.Contains(compositionProject))
            {
                throw new InvalidOperationException(string.Format(SR.ProjectManager_LoadProject_InvalidOperation, InvalidOperationDuplicateProject));
            }

            //
            // 今天加到目录
            _projectDistinct.Add(compositionProject);
            
            //
            //
            Editable.Add(compositionProject);

            if (!isOpen)
            {
                return;
            }
            
            if (_current is not null)
            {
                //
                // 释放之前的内容
                await _mediator.Publish(new DocumentCloseNotification());
                _current.Dispose();
            }
                
            //
            // 添加到集合当中
            Editable.Add(compositionProject);
                
            //
            //
            _current = compositionProject;

            //
            // 推送更改
            await _mediator.Publish(new DocumentOpenNotification(compositionProject));
            
            //
            //
            CurrentProjectStream.OnNext(compositionProject);
        }

        public async Task CloseProject()
        {
            if (_current is null)
            {
                return;
            }

            //
            // 通知注销
            await _mediator.Publish(new DocumentCloseNotification());

            //
            //
            _current?.Dispose();
            _current = null;
            
            //
            //
            CurrentProjectStream.OnNext(null);
        }

        internal IDocumentEngineAquirement Aquirement => _queue;

        public IObservable<ICompositionSet> CurrentProject => CurrentProjectStream;
    }
}