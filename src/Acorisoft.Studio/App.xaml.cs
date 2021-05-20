using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Studio.ViewModels;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    public class StudioBootstrapper : Bootstrapper
    {
        public StudioBootstrapper() : base()
        {
            Container.EnableStartup(() => this);
        }

        protected override async void OnStartup()
        {
            await Await<
        }
    }
}