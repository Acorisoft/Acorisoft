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

namespace Acorisoft.Morisa.Routers
{
    [Obsolete]
    class InternalViewManager : IScreen
    {
        // Mock ViewManager
        public InternalViewManager()
        {
            Router = new RoutingState();
        }

        public RoutingState Router { get; }
    }

    [Obsolete]
    public static class RouterExtensions
    {
        private static InternalViewManager _mgr;
        private static IFullLogger _logger;
        private static readonly NavigationPipeline _pipeline;
        private static IRoutableViewModel _old;



        private class NavigationPipeline : INavigationPipeline
        {
            public NavigationPipeline()
            {
                InternalFilters = new List<INavigationFilter>();
            }

            public void OnNext(NavigationFilterEventArgs e)
            {
                var iterator = InternalFilters.Reverse<INavigationFilter>().GetEnumerator();

                while(iterator.MoveNext())
                {
                    var filter = iterator.Current;
                    filter.Filter(e);
                    if(!e.CanNavigate)
                    {
                        break;
                    }
                }
            }

            internal List<INavigationFilter> InternalFilters { get; }

            public IReadOnlyCollection<INavigationFilter> Filters => InternalFilters;
        }

        static RouterExtensions()
        {
            _pipeline = new NavigationPipeline();
            _mgr = new InternalViewManager();
        }

        public static IApplicationEnvironment UseRouter(this IApplicationEnvironment appEnv)
        {
            _logger = (new InternalViewManager()).GetLogger();
            _mgr.Router.CurrentViewModel.Subscribe(OnViewModelChanged);
            Locator.CurrentMutable.RegisterConstant<IScreen>(_mgr);
            return appEnv;
        }

        private static void OnViewModelChanged(IRoutableViewModel vm)
        {
            //
            // 当vm为空的时候直接跳过当前环节
            if(vm == null)
            {
                return;
            }

            //
            // 如果过滤管线不为空，则将当前视图模型进入管线过滤
            if(_pipeline.Filters.Count > 0)
            {
                var e = new NavigationFilterEventArgs(_old, vm)
                {
                    Logger = _logger
                };

                _pipeline.OnNext(e);

                //
                // 导航到指定视图模型
                _mgr.Router.Navigate.Execute(e.Result);
                _logger.Info($"导航到页面：{e.Result.GetType().Name}");
            }
            else
            {
                _logger.Info($"导航到页面：{vm.GetType().Name}");
            }

            _old = vm;
        }

        public static void Subscribe(INavigationFilter observer)
        {
            _pipeline.InternalFilters.Add(observer);
        }

        public static void Unsubscribe(INavigationFilter observer)
        {
            _pipeline.InternalFilters.Remove(observer);
        }

        // public static RoutingState Router => _mgr.Router;
    }
}
