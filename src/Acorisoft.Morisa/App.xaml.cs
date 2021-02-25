﻿using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Logs;
using Acorisoft.Morisa.Routers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            _appEnv.UseLog()
                   .UseRouter();
        }
    }
}
