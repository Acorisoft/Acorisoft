using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Logs;
using Acorisoft.Morisa.Routers;
using Acorisoft.Morisa.Globalization;
using Acorisoft.Morisa.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using ReactiveUI;
using Acorisoft.Morisa.Views;
using Acorisoft.Morisa.Properties;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IApplicationEnvironment _appEnv;

        public App()
        {
            _appEnv = new ApplicationEnvironment();
            //_appEnv.Container.Register<IViewFor<DialogSampleViewModel>, DialogSampleView>();
            //_appEnv.Container.Register<IViewFor<InsertTextDialogViewModel>, InsertTextDialogView>();
            //_appEnv.Container.Register<DialogSampleViewModel>();
            //_appEnv.Container.Register<InsertTextDialogViewModel>();
            _appEnv.UseLog()
                   .UseRouter()
                   .UseDialog()
                   .UseViews(typeof(App).Assembly, typeof(Notification).Assembly)
                   .UseGlobalization(Language.ResourceManager,new System.Globalization.CultureInfo("zh"));
        }
    }
}
