using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Models;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.Core;
using LiteDB;
using Newtonsoft.Json;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        private readonly CompositeDisposable _disposable;
        private readonly IComposeSetSystem _css;
        private readonly IComposeSetFileSystem _fileManager;
        private readonly ObservableAsPropertyHelper<bool> _isOpen;
        private const string SettingFileName = "app.config";
        
        
        public AppViewModel(IViewService service,
            IComposeSetRequestQueue requestQueue, 
            IComposeSetFileSystem fileManager,
            IComposeSetSystem composeSetSystem) : base(service)
        {
            if (requestQueue == null)
            {
                throw new ArgumentNullException(nameof(requestQueue));
            }

            string json;
            if (!File.Exists(SettingFileName))
            {
                using (ViewAware.ForceBusyState("初始化应用设置"))
                {
                    try
                    {
                        //
                        // 如果应用设置不存在，则创建
                        Setting = new StudioSettingWrapper(new StudioSetting());
                        json = JsonConvert.SerializeObject(Setting.Source);
                        File.WriteAllText(SettingFileName, json);
                    }
                    catch
                    {
                        ViewAware.Toast("发生错误");
                    }
                }
            }
            else
            {
                json = File.ReadAllText(SettingFileName);
                Setting = new StudioSettingWrapper(JsonConvert.DeserializeObject<StudioSetting>(json));
            }

            
            
            var disposablePos = requestQueue.Requesting
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualStartBusyState("正在项目数据"); });

            var disposablePoe = requestQueue.Responding
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { service.ManualEndBusyState(); });

            _disposable = new CompositeDisposable();
            _fileManager = fileManager;
            _css = composeSetSystem;
            _isOpen = _css.IsOpen.ToProperty(this, nameof(IsOpen));
            
            
            _disposable.Add(_css);
            disposablePoe.DisposeWith(_disposable);
            disposablePos.DisposeWith(_disposable);
        }

        private void WriteSetting()
        {
            var json = JsonConvert.SerializeObject(Setting.Source);
            File.WriteAllText(SettingFileName, json);
        }

        protected override async void OnStart()
        {
            if (Setting.RecentProject != null)
            {
                using (ViewAware.ForceBusyState("打开最近的项目"))
                {
                    try
                    {
                        await _css.OpenAsync(Setting.RecentProject);
                    }
                    catch
                    {
                        ViewAware.Toast("打开失败");
                    }
                }
            }
        }

        protected override async void OnStop()
        {
            await _css.CloseAsync();
            _disposable.Dispose();
            WriteSetting();
        }

        /// <summary>
        /// 获取或设置当前的可打开项目。
        /// </summary>
        public bool IsOpen => _isOpen.Value;
        public StudioSettingWrapper Setting { get; }
    }
}