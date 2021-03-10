using Acorisoft.Morisa.Dialogs;
using DryIoc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat.DryIoc;
using System.Reflection;
using System.Diagnostics;
using Splat;
using NLog;
using Splat.NLog;
using LogLevel = NLog.LogLevel;
using NLog.Config;
using NLog.Targets;
using Acorisoft.Morisa.ViewModels;

namespace Acorisoft.Morisa
{
    public interface IViewManager : IScreen
    {
        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        /// <typeparam name="TViewModel">指定当前视图管理器将要导航到的视图模型类型。</typeparam>
        void View<TViewModel>() where TViewModel : IRoutableViewModel;

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        /// <typeparam name="TViewModel">指定当前视图管理器将要导航到的视图模型类型。</typeparam>
        void View<TViewModel>(NavigationParameter @params) where TViewModel : IRoutableViewModel;

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        void View(IRoutableViewModel vm);

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        void View(IRoutableViewModel vm , NavigationParameter @params);

        IFullLogger Logger { get; }
    }

    public class NavigationParameter
    {
    }

    public class NavigationEventArgs : EventArgs
    {
        private IRoutableViewModel _result;

        public NavigationEventArgs(IRoutableViewModel oldVM , IRoutableViewModel newVM , IFullLogger logger)
        {
            Old = oldVM;
            New = newVM ?? throw new ArgumentNullException(nameof(newVM));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Result = New;
        }

        /// <summary>
        /// 获取当前过滤器的日志记录工具。
        /// </summary>
        public IFullLogger Logger { get; private set; }

        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel Old { get; }

        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel New { get; }

        /// <summary>
        /// 获取或设置当前导航过滤事件的最终导航结果，该属性由管线过滤器设置。
        /// </summary>
        public IRoutableViewModel Result { get; set; }
    }

    public static class ShellMixins
    {
        class ViewManager : IViewManager, IScreen
        {
            private readonly RoutingState                   _router;
            private IRoutableViewModel                      _oldVM;
            private static IFullLogger                      _logger;
            private readonly Stack<object>                  _instanceStack;
            private readonly Stack<NavigationParameter>     _paramStack;


            public ViewManager()
            {
                _router = new RoutingState();
                _instanceStack = new Stack<object>();
                _paramStack = new Stack<NavigationParameter>();
                _router.CurrentViewModel.Subscribe(OnViewModelChanged);
                if (_logger == null)
                {
                    _logger = this.GetLogger();
                }
            }

            protected void OnViewModelChanged(IRoutableViewModel vm)
            {
                if (vm is ViewModelBase vmBase &&
                    _instanceStack.Count > 0 &&
                    ReferenceEquals(_instanceStack.Peek() , vm))
                {
                    _instanceStack.Pop();
                    // vmBase.Initialize(_paramStack.Pop());
                }
            }

            protected void OnPreExecute(NavigationEventArgs e)
            {

            }

            protected void OnPostExecute(NavigationEventArgs e)
            {

            }

            public void View<TViewModel>() where TViewModel : IRoutableViewModel
            {
                var vm = (IRoutableViewModel)Locator.Current.GetService<TViewModel>();
                var e = new NavigationEventArgs(_oldVM, vm, this.GetLogger());
                OnPreExecute(e);
                _router.Navigate.Execute(e.Result);
                OnPostExecute(e);
                _oldVM = vm;
            }

            public void View<TViewModel>(NavigationParameter @params) where TViewModel : IRoutableViewModel
            {
                var vm = (IRoutableViewModel)Locator.Current.GetService<TViewModel>();
                var e = new NavigationEventArgs(_oldVM, vm, this.GetLogger());
                _instanceStack.Push(vm);
                _paramStack.Push(@params);
                OnPreExecute(e);
                _router.Navigate.Execute(e.Result);
                OnPostExecute(e);
                _oldVM = vm;
            }

            public void View(IRoutableViewModel vm)
            {
                var e = new NavigationEventArgs(_oldVM, vm, this.GetLogger());
                OnPreExecute(e);
                _router.Navigate.Execute(e.Result);
                OnPostExecute(e);
                _oldVM = vm;
            }

            public void View(IRoutableViewModel vm , NavigationParameter @params)
            {
                var e = new NavigationEventArgs(_oldVM, vm, this.GetLogger());
                _instanceStack.Push(vm);
                _paramStack.Push(@params);
                OnPreExecute(e);
                _router.Navigate.Execute(e.Result);
                OnPostExecute(e);
                _oldVM = vm;
            }

            public RoutingState Router => _router;
            public IFullLogger Logger => _logger;
        }

        private static IViewManager _vMgr;
        private static Type IViewForType = typeof(IViewFor<>);
        private static ILogManager _manager;

        /// <summary>
        /// 启用日志工具
        /// </summary>
        /// <param name="env">指定一个应用程序环境实例。要求不能为空。</param>
        public static IContainer UseLog(this IContainer container)
        {
            //
            // 创建日志配置文件
            var config = new LoggingConfiguration();

            //
            // 创建文件日志目标。
            var logFileTarget = new FileTarget("logFile")
            {
                FileName = "log.log"
            };
            //
            // 创建控制台日志目标
            var logConsoleTarget = new ConsoleTarget("logConsole");

            //
            // 添加过滤规则
            config.AddRule(LogLevel.Info , LogLevel.Fatal , logFileTarget);
            config.AddRule(LogLevel.Debug , LogLevel.Fatal , logConsoleTarget);

            //
            // 应用设置
            LogManager.Configuration = config;

            //
            // 设置使用NLog作为日志工具
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();

            //
            // 获取日志管理器。
            _manager = Locator.Current.GetService<ILogManager>();

            return container;
        }

        /// <summary>
        /// 获取指定的对象的日志记录工具。
        /// </summary>
        /// <param name="instance">指定要获取日志记录工具的类型。要求不能为空。</param>
        /// <returns>返回一个新的 <see cref="IFullLogger"/> 类型实例。</returns>
        public static IFullLogger GetLogger(this object instance)
        {
            return _manager.GetLogger(instance?.GetType() ?? typeof(ShellMixins));
        }

        public static IContainer UseViews(this IContainer container , params Assembly[] assemblies)
        { 
            _vMgr = new ViewManager();
            container.RegisterInstance(_vMgr);
            OnWireViewModel(container , assemblies);
            return container;
        }

        private static void OnWireViewModel(IContainer container , params Assembly[] assemblies)
        {
            var counter = new Stopwatch();
            counter.Start();

            if (assemblies != null)
            {
                var allTypes = new Dictionary<string, Type>(256);
                var viewTypes = new List<Type>();
                foreach (var assembly in assemblies)
                {
                    var typesInAssembly = assembly.GetTypes().Where(x => x.IsClass && x.Name.Contains("View"));

                    foreach (var type in typesInAssembly)
                    {
                        if (type.Name.EndsWith("View" , StringComparison.OrdinalIgnoreCase))
                        {
                            viewTypes.Add(type);
                        }
                        else
                        {
                            allTypes.Add(type.Name , type);
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
                    if (allTypes.TryGetValue(vType.Name + "Model" , out var vmType))
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
                            null ,
                            @params);
                        container.Register(vmType);
                    }

                }
            }
            counter.Stop();
            _vMgr.Logger.Info($"视图关联花费了:{counter.ElapsedMilliseconds}ms,总计:{counter.ElapsedTicks}ticks");
        }

        public static RoutingState Router {
            get {
                return _vMgr.Router;
            }
        }

        public static void View<TViewModel>() where TViewModel : IRoutableViewModel
        {
            _vMgr.View<TViewModel>();
        }
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
