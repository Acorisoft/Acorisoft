﻿using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public static class DialogueExtensions
    {
        public static IApplicationEnvironment UseDialog(this IApplicationEnvironment appEnv)
        {
            var container = appEnv.Container;

            return appEnv;
        }
    }
}
