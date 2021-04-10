using Acorisoft.Properties;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    public class ViewManager
    {
        #region Internal Class

        internal class NavigateContext : INavigateContext
        {
            private readonly IRoutableViewModel lastVM;
            private readonly INavigateParameter lastParams;
            private readonly IRoutableViewModel currentVM;
            private readonly INavigateParameter currentParams;

            public NavigateContext(IRoutableViewModel lastVM, INavigateParameter lastParams, IRoutableViewModel currentVM, INavigateParameter currentParams)
            {
                this.lastVM = lastVM;
                this.lastParams = lastParams;
                this.currentVM = currentVM;
                this.currentParams = currentParams;
            }

            public IRoutableViewModel NavigateIntend { get; set; }

            public IRoutableViewModel NavigateFrom => lastVM;

            public IRoutableViewModel NavigateTo => currentVM;

            public INavigateParameter NavigateFromParameters => lastParams;

            public INavigateParameter NavigateToParameters => currentParams;
        }

        #endregion Internal Class

        private readonly List<INavigatePipeline>    _ViewPipelines;
        private readonly IFullLogger                _ViewLogger;
        private readonly RoutingState               _ViewRouter;
        private IRoutableViewModel _LastVM;
        private INavigateParameter _LastParams;


        public ViewManager(ILogManager logMgr)
        {
            _ViewRouter = new RoutingState();
            _ViewLogger = logMgr.GetLogger(typeof(ViewManager));
            _ViewPipelines = new List<INavigatePipeline>();
        }

        protected void DisplayViewFor(IRoutableViewModel vm)
        {
            //
            // 日志记录最终完成的操作
            _ViewLogger.Info(string.Format(SR.View_Navigated, vm.GetType().Name));

            //
            // 导航到指定视图。
            _ViewRouter.Navigate.Execute(vm);
        }

        public void DisplayViewFor<TViewModel>(INavigateParameter parameters) where TViewModel : ViewModelBase
        {
            if (Locator.Current.GetService<TViewModel>() is IRoutableViewModel currentVM)
            {
                DisplayViewFor(currentVM, parameters);
            }
            else
            {
                _ViewLogger.Info(string.Format(SR.View_CannotFoundViewModel, typeof(TViewModel).Name));
            }
        }


        public void DisplayViewFor(Type vm, INavigateParameter parameters)
        {
            if (vm == null)
            {
                _ViewLogger.Info($"{SR.View_NavigateToNullViewModel}");
            }

            if (vm.IsAssignableTo(typeof(IRoutableViewModel)))
            {
                DisplayViewFor((IRoutableViewModel)Locator.Current.GetService(vm), parameters);
            }
            else
            {
                _ViewLogger.Info(string.Format(SR.View_CannotFoundViewModel, vm.Name));
            }
        }

        public void DisplayViewFor(IRoutableViewModel vm, INavigateParameter parameters)
        {
            if (vm == null)
            {
                _ViewLogger.Info($"{SR.View_NavigateToNullViewModel}");
            }

            var currentVM = vm;
            var currentParams = parameters;

            //
            // 构造一个导航上下文
            var context = new NavigateContext(
                _LastVM,
                _LastParams,
                currentVM,
                currentParams);

            INavigateTo toVMware;

            //
            // 日志记录当前导航的目标视图
            _ViewLogger.Info(string.Format(SR.View_Navigating, context.NavigateTo.GetType().Name));

            //
            // 进入管线过滤
            foreach (var pipeline in _ViewPipelines)
            {
                pipeline.OnNext(context);

                if (context.NavigateIntend is not null && !ReferenceEquals(context.NavigateIntend, context.NavigateTo))
                {
                    //
                    // 日志记录
                    _ViewLogger.Info(string.Format(SR.View_NavigateRedirection, context.NavigateIntend.GetType().Name));

                    //
                    // 如果是 INavigateTo 类型，则调用 INavigateTo 方法
                    if (context.NavigateIntend is INavigateTo)
                    {
                        toVMware = (INavigateTo)context.NavigateIntend;
                        toVMware.NavigateTo(context);
                    }

                    //
                    // 直接导航，不进行视图级过滤
                    DisplayViewFor(context.NavigateIntend);

                    //
                    // 上一个视图模型和导航参数
                    _LastVM = context.NavigateIntend;
                    _LastParams = context.NavigateToParameters;

                    //
                    // 结束管线
                    break;
                }

            }

            //
            // 进入视图模型级别的过滤
            if (context.NavigateTo is INavigateTo)
            {
                toVMware = (INavigateTo)context.NavigateTo;
                toVMware.NavigateTo(context);
            }

            //
            // 如果过滤了
            if (context.NavigateIntend is not null && !ReferenceEquals(context.NavigateIntend, context.NavigateTo))
            {
                //
                // 日志记录
                _ViewLogger.Info(string.Format(SR.View_NavigateRedirection, context.NavigateIntend.GetType().Name));

                //
                // 如果是 INavigateTo 类型，则调用 INavigateTo 方法
                if (context.NavigateIntend is INavigateTo)
                {
                    toVMware = (INavigateTo)context.NavigateIntend;
                    toVMware.NavigateTo(context);
                }

                //
                // 直接导航，不进行视图级过滤
                DisplayViewFor(context.NavigateIntend);

                //
                // 上一个视图模型和导航参数
                _LastVM = context.NavigateIntend;
                _LastParams = context.NavigateToParameters;
            }
            else
            {
                DisplayViewFor(context.NavigateTo);
            }
        }
    }
}
