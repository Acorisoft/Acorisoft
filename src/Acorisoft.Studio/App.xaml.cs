using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Platforms.Languages;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ProjectSystems;
using Acorisoft.Studio.Properties;
using Acorisoft.Studio.ViewModels;
using Acorisoft.Studio.Views;
using DryIoc;
using LiteDB;
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
        private readonly IContainer _container;
        public App()
        {
            var container = Platform.Init().UseMorisa();
            var css = ComposeSetSystem.Create(container);
            _container = container;
            container.Register<IViewFor<MockupDialogViewModel>,MockupView>();
            container.Register<AppViewModel>();
            container.RegisterInstance<ILanguageService>(new LanguageService(SR.ResourceManager));

            //
            // HomeViews

            container.RegisterStickyNote();
            container.RegisterCommonViewModelAndViews();
            RegisterViewAndViewModel<HomeView,HomeViewModel>();
            RegisterViewAndViewModel<HomeContextView,HomeContextViewModel>();
            RegisterDialogAndViewModel<NewProjectDialog,NewProjectDialogViewModel>();
        }

        protected void RegisterViewAndViewModel<TView, TViewModel>() where TViewModel : ViewModelBase where TView : IViewFor<TViewModel>
        {
            
            _container.Register<IViewFor<TViewModel>, TView>();
            _container.Register<TViewModel>();
        }
        
        protected void RegisterDialogAndViewModel<TView, TViewModel>() where TViewModel : DialogViewModelBase where TView : IViewFor<TViewModel>
        {
            
            _container.Register<IViewFor<TViewModel>, TView>();
            _container.Register<TViewModel>();
        }
    }
}