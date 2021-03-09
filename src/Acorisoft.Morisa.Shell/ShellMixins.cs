using Acorisoft.Morisa.Dialogs;
using DryIoc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat.DryIoc;

namespace Acorisoft.Morisa
{
    public static class ShellMixins
    {
        public static IContainer Init(this IContainer container)
        {
            container.UseDryIocDependencyResolver();
            return container;
        }

        public static IContainer UseDialog(this IContainer container)
        {
            container.Register<Notification>();
            container.Register<IViewFor<Notification>, NotificationView>();
            return container;
        }
    }
}
