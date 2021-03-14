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
                      .UseMorisa()
                      .UseLog()
                      .UseViews(typeof(App).Assembly)
                      .UseDialog();
            AppViewModel = Locator.Current.GetService<AppViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if(e.Args.Length > 0)
            {
                
            }
            else
            {
                ShellMixins.View<HomeViewModel>();
            }

            //
            // 启动应用
            base.OnStartup(e);
        }

        public AppViewModel AppViewModel { get; }
    }
}
