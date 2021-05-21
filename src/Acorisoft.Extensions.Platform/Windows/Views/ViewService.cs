using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    internal class ViewService : IViewService
    {
        private IPageViewModel _lastViewModel;
        private readonly Stack<IPageViewModel> _viewModelStack;
        private readonly object _sync;

        public ViewService()
        {
            _viewModelStack = new Stack<IPageViewModel>();
            _sync = new object();
        }

        public bool CanGoBack => _viewModelStack.Count > 0;

        public void GoBack()
        {
            //
            // 构建事件参数
            var lastViewModel = _viewModelStack.Pop();
            var currentViewModel = _lastViewModel;
            var eventArgs = new NavigateToViewEventArgs(lastViewModel, currentViewModel);

            //
            // 进入 IPageController 控制器中过滤无效的请求。
            var controller = (IPageController) ServiceProvider.Provider.GetService(typeof(IPageController));

            lock (_sync)
            {
                _lastViewModel = lastViewModel;
            }

            if (controller is not null && controller.CanNavigate(eventArgs))
            {
                Navigating?.Invoke(this, eventArgs);
            }
        }

        public void NavigateTo(ISplashViewModel vm)
        {
            //
            // 构建事件参数
            var eventArgs = new NavigateToViewEventArgs(_lastViewModel, vm);

            Navigating?.Invoke(this, eventArgs);
        }

        public void NavigateTo(IPageViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            //
            // 进入 IPageController 控制器中过滤无效的请求。
            var controller = (IPageController) ServiceProvider.Provider.GetService(typeof(IPageController));

            //
            // 构建事件参数
            var eventArgs = new NavigateToViewEventArgs(_lastViewModel, vm);

            //
            // 是否跳转，默认为跳转
            var ensureNavigating = true;


            if (controller is not null)
            {
                //
                // 先过滤空的视图模型
                //
                // TODO : viewmodel filtering
                ensureNavigating = controller.CanNavigate(eventArgs);
            }

            if (!ensureNavigating)
            {
                return;
            }

            lock (_sync)
            {
                _lastViewModel = vm;

                //
                // 如果KeepAlive为真则表示当前视图应该出现在堆栈中，这个堆栈是自己维护的，而不是使用ReactiveUI维护的视图堆栈。
                if (vm.KeepAlive)
                {
                    _viewModelStack.Push(vm);
                }
            }

            Navigating?.Invoke(this, eventArgs);
        }

        public Task Waiting(Action operation, string description)
        {
            var mre = new ManualResetEventSlim();
            var args = new IsBusyEventArgs(mre);
            IsBusy?.Invoke(this, args);
            BusyStateChanged?.Invoke(this, description);

            return Task.Run(() =>
            {
                operation?.Invoke();
                mre.Set();
                mre.Dispose();
            });
        }

        public Task Waiting(IEnumerable<Tuple<Action, string>> operations)
        {
            var mre = new ManualResetEventSlim();
            var args = new IsBusyEventArgs(mre);
            IsBusy?.Invoke(this, args);
            return Task.Run(() =>
            {
                foreach (var operation in operations)
                {
                    if (operation is null)
                    {
                        continue;
                    }
                    BusyStateChanged?.Invoke(this, operation.Item2);
                    operation.Item1?.Invoke();
                }

                mre.Set();
                mre.Dispose();
            });
        }

        /*
         * service part:
         * return Task.Run(()=>{ operation?.Invoke(); mre.Set();};
         * 
         */
        public event IsBusyEventHandler IsBusy;
        public event BusyStateChangedEventHandler BusyStateChanged;

        public event NavigateToViewEventHandler Navigating;
    }
}