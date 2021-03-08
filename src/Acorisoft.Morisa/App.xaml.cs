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
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //
            // 启动应用
            base.OnStartup(e);
        }

    }
}
