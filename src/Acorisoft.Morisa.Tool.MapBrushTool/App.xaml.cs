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

            var counter = new Stopwatch();
            var vmgr = _container.Resolve<IViewManager>();
            counter.Start();

            RegisterDialogs(_container);
            RegisterViews(_container);

            counter.Stop();
            vmgr.Logger.Info($"视图注册花费了:{counter.ElapsedMilliseconds}ms,总计:{counter.ElapsedTicks}ticks");
            AppViewModel = Locator.Current.GetService<AppViewModel>();
        }

        protected virtual void RegisterDialogs(IContainer container)
        {
            //container.Register<GenerateCompositionSetViewModel>();
            //container.Register<SelectProjectDirectoryViewModel>();

            //container.Register<IViewFor<GenerateCompositionSetViewModel>, GenerateCompositionSetView>();
            //container.Register<IViewFor<SelectProjectDirectoryViewModel>, SelectProjectDirectoryView>();
        }

        protected virtual void RegisterViews(IContainer container)
        {
            container.Register<HomeViewModel>();
            //container.Register<EmotionViewModel>();

            container.Register<IViewFor<HomeViewModel>, HomeView>();
            //container.Register<IViewFor<EmotionViewModel>, EmotionView>();
        }

        public AppViewModel AppViewModel { get; }
    }
}
