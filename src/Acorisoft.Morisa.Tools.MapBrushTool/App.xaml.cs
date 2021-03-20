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
                      .UseMorisa()
                      .UseLog()
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

        protected virtual void RegisterViews(IContainer container)
        {
            container.Register<HomeViewModel>();
            container.Register<NewBrushSetDialogViewModel>();
            container.Register<NewBrushSetDialogStep2ViewModel>();
            container.Register<NewBrushSetDialogStep3ViewModel>();
            container.Register<OpenBrushSetViewFunction>();
            container.Register<IViewFor<HomeViewModel>, HomeView>();
            container.Register<IViewFor<NewBrushSetDialogViewModel>, NewBrushSetDialogView>();
            container.Register<IViewFor<NewBrushSetDialogStep2ViewModel>, NewBrushSetDialogStep2View>();
            container.Register<IViewFor<NewBrushSetDialogStep3ViewModel>, NewBrushSetDialogStep3View>();
            container.Register<IViewFor<OpenBrushSetViewFunction>, OpenBrushSetView>();
            container.Register<IViewFor<NewBrushGroupViewFunction>, NewBrushGroupView>();
        }

        public AppViewModel AppViewModel { get; }
    }
}
