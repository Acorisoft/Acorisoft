using Acorisoft.Dialogs;
using Acorisoft.ViewModels;
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

namespace Acorisoft
{
#pragma warning disable IDE0001

    public static class WindowsShellExtensions
    {
        public static IContainer UseDialog(this IContainer container)
        {
            container.RegisterInstance<IDialogManager>(new DialogManager(container.Resolve<ILogManager>()));
            return container;
        }

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
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFileTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsoleTarget);

            //
            // 应用设置
            LogManager.Configuration = config;

            //
            // 设置使用NLog作为日志工具
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();

            return container;
        }

        public static IContainer UseViewManager(this IContainer container, Func<AppViewModel> factory)
        {
            var appVM = factory?.Invoke() ?? null;

            if (appVM == null)
            {
                return container;
            }

            container.RegisterInstance<IViewManager>(appVM);
            container.RegisterInstance<AppViewModel>(appVM);
            return container;
        }
    }
}
