using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using Splat.DryIoc;
using MediatR;
using System.Reflection;
using System.Diagnostics;
using Splat;
using NLog;
using Splat.NLog;
using LogLevel = NLog.LogLevel;
using NLog.Config;
using NLog.Targets;
using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.Core;
using static DryIoc.Rules;
using Acorisoft.Morisa.Tags;
using Acorisoft.Morisa.EventBus;

namespace Acorisoft.Morisa
{
    public static class MorisaSystem
    {
        public static IContainer Init()
        {
            return new Container(Rules.Default.WithTrackingDisposableTransients());
        }
        public static IContainer UseDryIoc(this IContainer container)
        {
            //
            //
            container.UseDryIocDependencyResolver();

            //
            // 注册所有System
            return container;
        }
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


            //
            // 注册所有System
            return container;
        }
        public static IContainer UseMorisa(this IContainer container)
        {
            //
            // 创建ServiceFactory代理
            container.RegisterDelegate<ServiceFactory>(f => f.Resolve);

            //
            // 创建 ICompositionSetMediator 中介者
            container.RegisterInstance<ICompositionSetMediator>(new CompositionSetMediator(container.Resolve<ServiceFactory>()));

            //
            // 创建 IDataPropertyManager 中介者
            container.RegisterInstance<IDataPropertyManager>(new DataPropertyManager(container));


            // container.RegisterInstance<IFileManager>(new FileManager());
            var logger = container.ResolveMany<ILogManager>()
                                  .LastOrDefault();

            //------------------------------------------------------------------------------------------------
            //
            //
            //
            //
            //
            //------------------------------------------------------------------------------------------------



            var mediator = container.Resolve<ICompositionSetMediator>();

            //
            //
            container.RegisterInstance<ICompositionSetManager>(new CompositionSetManager(
                mediator,
                logger,
                container.Resolve<IDataPropertyManager>()));

            //
            //
            RegisterTagFactory(container, mediator, logger);

            //
            // 注册所有System
            return container;
        }

        private static void RegisterTagFactory(IContainer container, ICompositionSetMediator mediator, ILogManager logger)
        {
            var factory = new TagFactory(mediator , logger);

            container.RegisterInstance<ITagFactory>(factory);
            container.UseInstance<INotificationHandler<CompositionSetOpeningInstruction>>(factory);
            container.UseInstance<INotificationHandler<CompositionSetClosingInstruction>>(factory);
        }
    }
}
