using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using ReactiveUI;
using Splat;
using Acorisoft.Morisa.Logs;
using Acorisoft.Morisa.Views;

namespace Acorisoft.Morisa.Dialogs
{
    public static class DialogueExtensions
    {
        public static IApplicationEnvironment UseDialog(this IApplicationEnvironment appEnv)
        {
            var container = appEnv.Container;
            container.RegisterInstance<IDialogService>(new DialogManager());
            container.Register<IViewFor<Notification>, NotificationView>();
            container.Register<IViewFor<MessageBox>, MessageBoxView>();
            return appEnv;
        }
    }
}
