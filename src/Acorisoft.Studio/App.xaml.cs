using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ViewModels;
using Acorisoft.Studio.Views;
using DryIoc;
using ReactiveUI;
using Splat;
using Splat.DryIoc;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var container = Platform.Init().UseMorisa();
            container.Register<IViewFor<MockupDialogViewModel>,MockupView>();
            container.Register<AppViewModel>();

            //
            // HomeViews

            container.Register<IViewFor<HomeViewModel>, HomeView>();
            container.Register<IViewFor<HomeContextViewModel>, HomeContextView>();
            container.Register<IViewFor<NewProjectDialogViewModel>, NewProjectDialog>();
            container.Register<HomeViewModel>();
            container.Register<HomeContextViewModel>();
            container.Register<NewProjectDialogViewModel>();
        }
    }
}