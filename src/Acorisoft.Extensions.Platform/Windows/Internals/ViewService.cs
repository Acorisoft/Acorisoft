using System;
using System.Collections.Generic;
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

        public void NavigateTo(IPageViewModel vm)
        {
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

        public event NavigateToViewEventHandler Navigating;
    }
}