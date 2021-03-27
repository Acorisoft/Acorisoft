using Acorisoft.Morisa.Tools.Views;
using Acorisoft.Morisa.Tools.ViewModels;
using Acorisoft.Morisa.ViewModels;
using DryIoc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using Acorisoft.Morisa;
using Splat;
using System.Diagnostics;
using Acorisoft.Morisa.Map;

namespace Acorisoft.Morisa.Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer _container;
        private readonly IFullLogger _logger;


        public App()
        {
            _container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _container.Init()
                      .UseLog()
                      .UseMorisa()
                      .UseViews(typeof(App).Assembly)
                      .UseDialog();
            
            //
            // 创建Logger
            _logger = this.GetLogger();

            AppDomain.CurrentDomain.UnhandledException += OnAppdomainUnhandleException;
            DispatcherUnhandledException += OnApplicationunhandleException;

            RegisterDialogs(_container);
            RegisterViews(_container);
            AppViewModel = Locator.Current.GetService<AppViewModel>();
        }

        private void OnApplicationunhandleException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error($"致命的错误:{e.Exception.Message}");
            _logger.Error($"致命的错误:{e.Exception.StackTrace}");
            e.Handled = true;
        }

        private void OnAppdomainUnhandleException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error(e.ExceptionObject);
        }

        protected virtual void RegisterDialogs(IContainer container)
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShellMixins.View<HomeViewModel>();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }

        public AppViewModel AppViewModel { get; }
    }
}
