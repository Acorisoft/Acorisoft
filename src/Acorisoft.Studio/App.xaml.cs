using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.ViewModels;
using Acorisoft.Studio.ViewModels;
using DryIoc;

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
        }

        protected override async void OnStartup()
        {
            //
            // Navigate to an mock view model,and do not show any real view when waiting startup operation done.
            //
            Platform.ViewService.NavigateTo(Container.Resolve<SplashViewModel>());
            await Platform.ViewService.Waiting(new Tuple<Action,string>[]
            {
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 1"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 2"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 3"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 4"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 5"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 6"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 7"),
                new Tuple<Action, string>(()=> Thread.Sleep(1500), "Operation 8"),
            });
        }
    }
}