using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using DryIoc;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        private readonly IContainer _container;
        public App()
        {
            var container = Platform.Init();
            _container = container;
        }
    }
}