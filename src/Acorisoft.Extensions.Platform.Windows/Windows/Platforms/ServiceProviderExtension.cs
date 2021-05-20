using DryIoc;
using Splat.DryIoc;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DryIoc;
using ReactiveUI;
using DryIoc;
using NLog;
using NLog.Config;
using NLog.Targets;
using ReactiveUI;
using Splat;
using Splat.NLog;
using System;
using System.Collections;
using System.IO;
using LogLevel = NLog.LogLevel;

namespace Acorisoft.Extensions.Windows.Platforms
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 初始化容器服务
        /// </summary>
        /// <returns></returns>
        public static IContainer Init()
        {
            var container = new Container(Rules.Default.WithTrackingDisposableTransients());
            container.UseDryIocDependencyResolver();
            container.RegisterInstance<IViewService>(new ViewService());
            container.RegisterInstance<IDialogService>(new DialogService());
            container.RegisterInstance<IToastService>(new ToastService());
            container.RegisterInstance<IInteractiveService>(new InteractiveService());
            ServiceProvider.SetServiceProvider(container);
            return container;
        }
        
        public static IContainer EnableStartup(this IContainer container, Func<IStartup> factory)
        {
            if (factory is not null)
            {
                container.RegisterInstance<IStartup>(factory());
            }
            return container;
        }

        public static void EnableLogger()
        {
            //
            // 创建日志配置文件
            var config = new LoggingConfiguration();
            //
            // 创建文件日志目标。
            var logFileTarget = new FileTarget("logFile")
            {
                FileName = Path.Combine(Environment.CurrentDirectory, "Logs", DateTime.Now.ToShortDateString())
            };
            //
            // 创建控制台日志目标
            var logConsoleTarget = new ConsoleTarget("logConsole");
            //
            // 添加过滤规则
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFileTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsoleTarget);
            //
            // 应用设置
            LogManager.Configuration = config;
            //
            // 设置使用NLog作为日志工具
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();
        }
    }
}