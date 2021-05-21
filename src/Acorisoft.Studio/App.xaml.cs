using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Windows;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.Threadings;
using Acorisoft.Extensions.Windows.ViewModels;
using Acorisoft.Extensions.Windows.Views;
using Acorisoft.Studio.ViewModels;
using Acorisoft.Studio.Views;
using DryIoc;
using ReactiveUI;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new StudioBootstrapper();
        }
    }

    public class StudioBootstrapper : Bootstrapper
    {
        public StudioBootstrapper() : base()
        {
            Container.EnableStartup(() => this);
            RegisterViews();
        }

        protected void RegisterViews()
        {
            Wire<HomeViewModel, HomeView>();
        }

        protected void Wire<TViewModel, TView>() where TViewModel : PageViewModel, IPageViewModel
            where TView : SpaPage<TViewModel>
        {
            Container.Register<TViewModel>();
            Container.Register<IViewFor<TViewModel>,TView>();
        }

        protected override async void OnStartup()
        {
            //
            // Navigate to an mock view model,and do not show any real view when waiting startup operation done.
            //
            await ViewAware.Waiting<SplashViewModel>(new ObservableOperation[]
            {
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation Loading"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation1"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation3"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation4"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation4"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation5"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation6"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation7"),
                new ObservableOperation(()=>Thread.Sleep(1500),"Operation8"),
            });
            ViewAware.NavigateTo<HomeViewModel>();
        }
    }
}