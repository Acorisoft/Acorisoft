using Acorisoft.Morisa.ViewModels;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer _container;
        private readonly object _appEnv;

        public App()
        {
            _container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _container.Register<AppViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //
            // 应用级别的视图模型。
            AppViewModel = _container.Resolve<AppViewModel>();

            //
            // 启动应用
            base.OnStartup(e);
        }

        public AppViewModel AppViewModel { get; private set; }
    }
}
