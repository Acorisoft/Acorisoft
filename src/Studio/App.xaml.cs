using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Acorisoft.Spa;
using DryIoc;
using ReactiveUI;
using SPA = Acorisoft.Spa.Spa;
namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : SPA
    {
        protected override void RegisterPipeline(IViewConductor conductor)
        {
            base.RegisterPipeline(conductor);
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<IViewFor<TestViewModel>, TestView>();
            container.Register<IViewFor<DialogViewModel>, TestDialog>();
            container.Register<DialogViewModel>();
            base.RegisterServices(container);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var conductor = Ioc.Get<IViewConductor>();
            conductor.Produce(Intent.Create<TestViewModel>());
            base.OnStartup(e);
        }
    }
}
