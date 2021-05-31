using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.ProjectSystems;
using LiteDB;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        private readonly CompositeDisposable _disposable;
        private readonly IComposeSetSystem _css;
        private readonly IComposeSetFileSystem _fileManager;
        private readonly ObservableAsPropertyHelper<bool> _isOpen;
        private readonly LiteDatabase _database;
        
        
        
        public AppViewModel(IViewService service,
            IComposeSetRequestQueue requestQueue, 
            IComposeSetFileSystem fileManager,
            IComposeSetSystem composeSetSystem) : base(service)
        {
            if (requestQueue == null)
            {
                throw new ArgumentNullException(nameof(requestQueue));
            }

            _database = new LiteDatabase(new ConnectionString
            {
                Filename = "App.Setting"
            });
            
            var disposablePos = requestQueue.Requesting
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualStartBusyState("打开项目"); });

            var disposablePoe = requestQueue.Responding
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualEndBusyState(); });

            _disposable = new CompositeDisposable();
            _fileManager = fileManager;
            _css = composeSetSystem;
            _isOpen = _css.IsOpen.ToProperty(this, nameof(IsOpen));
            
            
            _disposable.Add(_database);
            _disposable.Add(_css);
            disposablePoe.DisposeWith(_disposable);
            disposablePos.DisposeWith(_disposable);
        }

        protected override async void OnStart()
        {
            const string path = @"D:\BT\Hosting";
            
            //
            // Create
            if (!Directory.Exists(path))
            {
                await _css.NewAsync(new NewItemInfo<IComposeSetProperty>(new ComposeSetProperty())
                {
                    Path = path,
                    Name = "测试"
                });
            }
            else
            {
                await _css.OpenAsync(new ComposeProject
                {
                    Path = path,
                });
            }
        }

        protected override async void OnStop()
        {
            await _css.CloseAsync();
            _disposable.Dispose();
        }

        /// <summary>
        /// 获取或设置当前的可打开项目。
        /// </summary>
        public bool IsOpen => _isOpen.Value;
    }
}