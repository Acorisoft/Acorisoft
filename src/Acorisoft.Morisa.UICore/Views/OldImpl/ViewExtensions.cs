using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using System.Reflection;
using System.Diagnostics;
using ReactiveUI;
using Acorisoft.Morisa.Logs;
using Splat;

namespace Acorisoft.Morisa.Views
{
    [Obsolete]
    class ViewBinder
    {

    }

    [Obsolete]
    public static class ViewExtensions
    {
        private static Type HomePageType;
        private static Type IViewForType = typeof(IViewFor<>);


        public static IApplicationEnvironment UseViews2(this IApplicationEnvironment appEnv, Assembly assembly)
        {
            var container = appEnv.Container;
            var counter = new Stopwatch();
            counter.Start();

            //
            // 反射所有类类型
            var allClassTypes = assembly.GetTypes()
                                        .Where(x => x.IsClass && x.Name.Contains("View"));

            var @params = new object[]
                        {
                            container,
                            (IReuse)null,
                            (Made)null,
                            (Setup) null,
                            (IfAlreadyRegistered? ) null,
                            null
                        };

            var genericParams = new Type[]
                        {
                            typeof(IRegistrator) ,
                            typeof(IReuse),
                            typeof(Made),
                            typeof(Setup),
                            typeof(IfAlreadyRegistered?),
                            typeof(object)
                        };

            var genericModifiers = new ParameterModifier[]{ new ParameterModifier(2) };
            //
            // 遍历所有类型
            foreach (var maybeView in allClassTypes)
            {
                if (maybeView.GetCustomAttribute<ViewModelAttribute>() is ViewModelAttribute attribute)
                {
                    var vmType = attribute.ViewModel;
                    var vType = maybeView;
                    var viewForInterface = IViewForType.MakeGenericType(vmType);
                    var paramTypes = new Type[]{viewForInterface,vType};
                    var register = typeof(Registrator).GetMethod(
                        "Register",
                        2,
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        CallingConventions.Standard,
                        genericParams,
                        genericModifiers);
                    register.MakeGenericMethod(paramTypes).Invoke(
                        null,
                        @params);
                    container.Register(vmType);
                }

                if (maybeView.GetCustomAttribute<HomePageAttribute>() is HomePageAttribute)
                {
                    HomePageType = maybeView;
                }
            }
            counter.Stop();
            new ViewBinder().GetLogger().Info($"视图模型绑定耗时:{counter.ElapsedMilliseconds}ms,总周期:{counter.ElapsedTicks}ticks");

            return appEnv;
        }

        public static void NavigateToHomePage()
        {
            var uncastVM = Locator.Current.GetService(HomePageType);
            var vm = (IRoutableViewModel)uncastVM;
            var screen = Locator.Current.GetService<IScreen>();
            screen.Router.Navigate.Execute(vm);
        }
    }
}
