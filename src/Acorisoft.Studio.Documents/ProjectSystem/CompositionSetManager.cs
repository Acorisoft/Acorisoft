using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using LiteDB;
using MediatR;

namespace Acorisoft.Studio.ProjectSystem
{
    public class CompositionSetManager : ICompositionSetManager
    {
        public const string DocumentDatabaseVersion1Suffix = ".Md2v1";
        public const string DocumentMainDatabaseName = "Main";
        public const string DocumentMainDatabaseFileName = DocumentMainDatabaseName + DocumentDatabaseVersion1Suffix;
        public const int DocumentMainDatabaseSize = 32 * 1024 * 1024;
        public const int DocumentMainDatabaseCacheSize = 32 * 1024 * 1024;
        public const string ImagesDirectory = "Images";
        public const string VideosDirectory = "Videos";
        public const string BrushesDirectory = "Brushes";
        public const string MapsDirectory = "Maps";

        private protected readonly SourceList<ICompositionSet> Editable;
        private protected readonly HashSet<ICompositionSet> HashSet;
        private protected readonly ReadOnlyObservableCollection<ICompositionSet> Bindable;
        private protected readonly Subject<ICompositionSet> CurrentComposition;
        private protected readonly Subject<bool> IsOpenStream;

        private ICompositionSet _current;
        private bool _isOpen;

        public CompositionSetManager(IMediator mediator, ICompositionSetPropertyManager propertyManager)
        {
            Queue = new CompositionSetRequestQueue();
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            CurrentComposition = new Subject<ICompositionSet>();
            Editable = new SourceList<ICompositionSet>();
            Editable.Connect().Bind(out Bindable).Subscribe();
            HashSet = new HashSet<ICompositionSet>();
            PropertyManager = propertyManager;
            IsOpenStream = new Subject<bool>();
        }

        private static string GetDatabaseFileNameFromPath(string path)
        {
            return Path.Combine(path, DocumentMainDatabaseFileName);
        }

        private static LiteDatabase GetDatabaseFromPath(string path)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = GetDatabaseFileNameFromPath(path),
                InitialSize = DocumentMainDatabaseSize,
                CacheSize = DocumentMainDatabaseCacheSize,
                Mode = LiteDB.FileMode.Exclusive
            });
        }

        public async Task Save()
        {
            if (!_isOpen)
            {
                return;
            }

            await Mediator.Publish(new CompositionSetSaveNotification());
        }

        private static string GetCompositionSetImagesDirectory(string path)
        {
            return Path.Combine(path, CompositionSet.ImagesDirectory);
        }

        private static string GetCompositionSetVideosDirectory(string path)
        {
            return Path.Combine(path, CompositionSet.VideosDirectory);
        }

        private static string GetCompositionSetBrushesDirectory(string path)
        {
            return Path.Combine(path, CompositionSet.BrushesDirectory);
        }

        private static string GetCompositionSetMapsDirectory(string path)
        {
            return Path.Combine(path, CompositionSet.MapsDirectory);
        }

        private static void MaintainProjectDirectory(string path)
        {
            var directories = new[]
            {
                GetCompositionSetBrushesDirectory(path),
                GetCompositionSetImagesDirectory(path),
                GetCompositionSetMapsDirectory(path),
                GetCompositionSetVideosDirectory(path)
            };

            foreach (var directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        public async void Dispose()
        {
            await CloseProject();

            foreach (var compositionSet in HashSet)
            {
                compositionSet.Dispose();
            }

            Editable.Clear();
        }

        /// <summary>
        /// 关闭当前正在打开的项目。
        /// </summary>
        public async Task CloseProject()
        {
            if (_current is CompositionSet compositionSet)
            {
                compositionSet.Dispose();
                compositionSet.MainDatabase = null;
            }

            _current = null;
            _isOpen = false;
            IsOpenStream.OnNext(false);
            await Mediator.Publish(new CompositionSetCloseNotification());
        }

        /// <summary>
        /// 加载一个项目。
        /// </summary>
        /// <param name="project">指示要加载的项目。</param>
        /// <param name="isOpen">指示是否打开。</param>
        public async Task LoadProject(ICompositionProject project, bool isOpen)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrEmpty(project.Name))
            {
            }

            if (string.IsNullOrEmpty(project.Path))
            {
            }

            if (!Directory.Exists(project.Path))
            {
                throw new InvalidOperationException("无法打开创作集，路径为空");
            }

            if (!File.Exists(GetDatabaseFileNameFromPath(project.Path)))
            {
                throw new InvalidOperationException("无法打开一个空的创作集");
            }

            try
            {
                var composition = new CompositionSet(project.Name, project.Path, project.Id);

                if (composition.Equals(_current))
                {
                    throw new InvalidOperationException("无法打开已经打开的项目");
                }

                if (!HashSet.Contains(composition))
                {
                    Editable.Add(composition);
                    HashSet.Add(composition);
                }


                if (!isOpen)
                {
                    return;
                }

                //
                // 关闭之前项目
                await CloseProject();

                //
                // 打开数据库
                var database = GetDatabaseFromPath(project.Path);

                //
                // 设置数据库
                composition.MainDatabase = database;

                //
                // 设置数据库
                PropertyManager.SetDatabase(database);

                //
                // 获取属性
                composition.Property = PropertyManager.GetProperty();

                //
                //
                _current = composition;
                _isOpen = true;
                
                IsOpenStream.OnNext(true);
                CurrentComposition.OnNext(composition);

                //
                // 通知更改
                await Mediator.Publish(new CompositionSetCloseNotification());
                await Mediator.Publish(new CompositionSetOpenNotification(composition));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("无法加载项目", ex);
            }
        }


        /// <summary>
        /// 加载并打开一个项目。
        /// </summary>
        /// <param name="composition">指示要加载的项目。</param>
        public async Task LoadProject(ICompositionSet composition)
        {
            if (composition == null)
            {
                throw new ArgumentNullException(nameof(composition));
            }

            if (string.IsNullOrEmpty(composition.Name))
            {
            }

            if (string.IsNullOrEmpty(composition.Path))
            {
            }

            if (!Directory.Exists(composition.Path))
            {
                throw new InvalidOperationException("无法打开创作集，路径为空");
            }

            if (!File.Exists(GetDatabaseFileNameFromPath(composition.Path)))
            {
                throw new InvalidOperationException("无法打开一个空的创作集");
            }

            try
            {
                if (composition.Equals(_current))
                {
                    throw new InvalidOperationException("无法打开已经打开的项目");
                }

                if (!HashSet.Contains(composition))
                {
                    Editable.Add(composition);
                    HashSet.Add(composition);
                }

                //
                // 关闭之前项目
                await CloseProject();

                //
                // 打开数据库
                var database = GetDatabaseFromPath(composition.Path);

                //
                // 设置数据库
                var composition1 = composition as CompositionSet;

                if (composition1 == null)
                {
                    return;
                }

                composition1.MainDatabase = database;

                //
                // 设置数据库
                PropertyManager.SetDatabase(database);

                //
                // 获取属性
                composition.Property = PropertyManager.GetProperty();

                //
                //
                _current = composition;
                _isOpen = true;
                IsOpenStream.OnNext(true);


                CurrentComposition.OnNext(composition);
                //
                // 通知更改
                await Mediator.Publish(new CompositionSetCloseNotification());
                await Mediator.Publish(new CompositionSetOpenNotification(composition1));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("无法加载项目", ex);
            }
        }

        /// <summary>
        /// 使用指定的参数创建一个新的创作集并打开。
        /// </summary>
        /// <param name="newProjectInfo">要传递的参数，要求不能为空。</param>
        /// <returns>返回等待此次操作完成的任务。</returns>
        public async Task NewProject(INewProjectInfo newProjectInfo)
        {
            if (newProjectInfo == null)
            {
                throw new ArgumentNullException(nameof(newProjectInfo));
            }

            if (string.IsNullOrEmpty(newProjectInfo.Name))
            {
                throw new ArgumentNullException("project name");
            }

            if (string.IsNullOrEmpty(newProjectInfo.Path))
            {
                throw new ArgumentNullException("project path");
            }

            if (File.Exists(GetDatabaseFileNameFromPath(newProjectInfo.Path)))
            {
                throw new InvalidOperationException("不能在一个已存在的项目中创建项目");
            }

            try
            {
                //
                // 判断目录是否存在
                if (!Directory.Exists(newProjectInfo.Path))
                {
                    Directory.CreateDirectory(newProjectInfo.Path);
                }

                //
                // 创建项目目录结构
                MaintainProjectDirectory(newProjectInfo.Path);

                //
                // 关闭之前的项目
                await CloseProject();

                //
                // 创建创作。
                var composition = new CompositionSet(newProjectInfo.Name, newProjectInfo.Path, Guid.NewGuid())
                {
                    MainDatabase = GetDatabaseFromPath(newProjectInfo.Path)
                };

                var compositionProperty = new CompositionSetProperty(newProjectInfo);

                composition.Property = compositionProperty;

                if (!HashSet.Add(composition))
                {
                    throw new InvalidOperationException("无法添加当前创作集到已打开的创作集集合当中");
                }

                Editable.Add(composition);

                //
                // 设置当前
                _current = composition;
                _isOpen = true;
                IsOpenStream.OnNext(true);

                //
                // 
                PropertyManager.SetDatabase(composition.MainDatabase);

                //
                // 设置属性
                await PropertyManager.SetProperty(compositionProperty);

                CurrentComposition.OnNext(composition);

                //
                // 通知更改
                await Mediator.Publish(new CompositionSetCloseNotification());
                await Mediator.Publish(new CompositionSetOpenNotification(composition));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("无法创建项目", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SetProperty(ICompositionSetProperty property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (!_isOpen)
            {
                return;
            }

            await PropertyManager.SetProperty(property);
        }

        /// <summary>
        /// 
        /// </summary>

        public IObservable<bool> IsOpen => IsOpenStream;

        /// <summary>
        /// 
        /// </summary>
        public ICompositionSetPropertyManager PropertyManager { get; }

        /// <summary>
        /// 获取当前创作集的请求队列。
        /// </summary>
        public ICompositionSetRequestQueue Queue { get; }

        /// <summary>
        /// 获取当前用户创建的所有创作集。
        /// </summary>
        public ReadOnlyObservableCollection<ICompositionSet> CompositionSets => Bindable;

        /// <summary>
        /// 获取当前正在打开的创作集。
        /// </summary>
        public IObservable<ICompositionSet> Composition => CurrentComposition;


        /// <summary>
        /// 获取或设置当前 <see cref="ICompositionSet"/> 的属性。
        /// </summary>
        public IObservable<ICompositionSetProperty> Property => PropertyManager.Property;

        /// <summary>
        /// 获取中介者
        /// </summary>
        public IMediator Mediator { get; }
    }
}