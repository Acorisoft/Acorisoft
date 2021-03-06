using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Logs;
using Acorisoft.Morisa.ViewModels;

namespace Acorisoft.Morisa.Views
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
            if(_logger == null)
            {
                _logger = this.GetLogger();
            }
        }

        protected void OnViewModelChanged(IRoutableViewModel vm)
        {
            if(vm is ViewModelBase vmBase && _instanceStack.Count > 0 && ReferenceEquals( _instanceStack.Peek(),vm))
            {
                _instanceStack.Pop();
                vmBase.Initialize(_paramStack.Pop());
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

        public void View(IRoutableViewModel vm, NavigationParameter @params)
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
    }
}
