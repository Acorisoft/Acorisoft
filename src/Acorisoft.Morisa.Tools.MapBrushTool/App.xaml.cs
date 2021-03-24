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

        public App()
        {
            _container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _container.Init()
                      .UseLog()
                      .UseMorisa()
                      .UseViews(typeof(App).Assembly)
                      .UseDialog();

            RegisterDialogs(_container);
            RegisterViews(_container);
            AppViewModel = Locator.Current.GetService<AppViewModel>();
        }

        protected virtual void RegisterDialogs(IContainer container)
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShellMixins.View<HomeViewModel>();
            base.OnStartup(e);
        }

        public AppViewModel AppViewModel { get; }
    }
}
