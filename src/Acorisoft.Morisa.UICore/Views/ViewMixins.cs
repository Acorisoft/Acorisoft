using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using ReactiveUI;
using System.Reflection;

namespace Acorisoft.Morisa.Views
{
    public static class ViewMixins
    {
        private static IViewManager _vMgr;
        private static Type IViewForType = typeof(IViewFor<>);

        public static IApplicationEnvironment UseViews(this IApplicationEnvironment appEnv, params Assembly[] assemblies)
        {
            var container = appEnv.Container;
            _vMgr = new ViewManager();
            container.RegisterInstance(_vMgr);
            OnWireViewModel(container, assemblies);
            return appEnv;
        }

        private static void OnWireViewModel(IContainer container, params Assembly[] assemblies)
        {
            if (assemblies != null)
            {
                var allTypes = new Dictionary<string, Type>(256);
                var viewTypes = new List<Type>();
                foreach (var assembly in assemblies)
                {
                    var typesInAssembly = assembly.GetTypes().Where(x => x.IsClass && x.Name.Contains("View"));

                    foreach (var type in typesInAssembly)
                    {
                        if (type.Name.EndsWith("View", StringComparison.OrdinalIgnoreCase))
                        {
                            viewTypes.Add(type);
                        }
                        else
                        {
                            allTypes.Add(type.Name, type);
                        }
                    }
                }

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

                var genericModifiers = new ParameterModifier[] { new ParameterModifier(2) };

                foreach (var vType in viewTypes)
                {
                    if (allTypes.TryGetValue(vType.Name + "Model", out var vmType))
                    {
                        var viewForInterface = IViewForType.MakeGenericType(vmType);
                        var paramTypes = new Type[] { viewForInterface, vType };
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
                    
                }
            }
        }

        public static RoutingState Router
        {
            get
            {
                return _vMgr.Router;
            }
        }

        public static void View<TViewModel>() where TViewModel : IRoutableViewModel
        {
            _vMgr.View<TViewModel>();
        }
    }
}
