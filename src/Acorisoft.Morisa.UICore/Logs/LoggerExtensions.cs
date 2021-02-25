using Acorisoft.Morisa.Core;
using NLog;
using NLog.Config;
using NLog.Targets;
using Splat;
using Splat.NLog;
using System;
using System.IO;
using LogLevel = NLog.LogLevel;

namespace Acorisoft.Morisa.Logs
{
    public static class LoggerExtensions
    {
        private static ILogManager _manager;

        /// <summary>
        /// 启用日志工具
        /// </summary>
        /// <param name="env">指定一个应用程序环境实例。要求不能为空。</param>
        public static IApplicationEnvironment UseLog(this IApplicationEnvironment env)
        {
            //
            // 创建日志配置文件
            var config = new LoggingConfiguration();

            //
            // 创建文件日志目标。
            var logFileTarget = new FileTarget("logFile");

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

            //
            // 获取日志管理器。
            _manager = Locator.Current.GetService<ILogManager>();

            return env;
        }

        /// <summary>
        /// 获取指定的对象的日志记录工具。
        /// </summary>
        /// <param name="instance">指定要获取日志记录工具的类型。要求不能为空。</param>
        /// <returns>返回一个新的 <see cref="IFullLogger"/> 类型实例。</returns>
        public static IFullLogger GetLogger(this object instance)
        {
            return _manager.GetLogger(instance?.GetType() ?? typeof(LoggerExtensions));
        }
    }
}
