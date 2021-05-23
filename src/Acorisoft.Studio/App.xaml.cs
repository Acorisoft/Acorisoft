﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Platforms.Services;
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
            var container = new Container();
            container.RegisterInstance<IViewService>(new ViewService());
            container.UseDryIocDependencyResolver();
            container.Register<IViewFor<MockupDialogViewModel>,MockupView>();
            
            ServiceProvider.SetServiceProvider(container);
        }
    }
}