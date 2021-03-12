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

namespace Acorisoft.Morisa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer _container;

        public App()
        {
            _container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _container.Init()
                      .UseLog()
                      .UseViews(typeof(App).Assembly)
                      .UseDialog();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //
            // 启动应用
            base.OnStartup(e);
        }

    }
}
