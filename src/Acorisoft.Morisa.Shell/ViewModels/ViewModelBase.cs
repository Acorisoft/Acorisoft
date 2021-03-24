
using Acorisoft.Morisa.Dialogs;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IViewManager, IDialogManager
    {
        private readonly Lazy<IScreen> _ScreenExpression;
        private readonly Lazy<IDialogManager> _DialogManagerExpression;
        private readonly Lazy<IViewManager> _ViewManagerExpression;

        protected ViewModelBase()
        {
            _ViewManagerExpression = new Lazy<IViewManager>(() => GetService<IViewManager>());
            _ScreenExpression = new Lazy<IScreen>(() => GetService<IScreen>());
            _DialogManagerExpression = new Lazy<IDialogManager>(() => GetService<IDialogManager>());
        }

        /// <summary>
        /// 设置指定字段的值并通知更改
        /// </summary>
        /// <typeparam name="T">指定要设置的字段类型。</typeparam>
        /// <param name="source">原始字段。</param>
        /// <param name="value">要设置的值。</param>
        /// <param name="name">设置新值的属性名，缺省。</param>
        protected bool Set<T>(ref T source, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(source, value))
            {
                this.RaisePropertyChanging(name);
                source = value;
                this.RaisePropertyChanged(name);

                return true;
            }

            return false;
        }

        protected static T GetService<T>()
        {
            return Locator.Current.GetService<T>();
        }


        /// <summary>
        /// 手动推送属性值正在变化的通知。
        /// </summary>
        /// <param name="name">指定属性值正在发生变化的属性名。</param>
        protected void RaiseUpdating(string name)
        {
            this.RaisePropertyChanging(name);
        }

        /// <summary>
        /// 手动推送属性值变化的通知。
        /// </summary>
        /// <param name="name">指定属性值发生变化的属性名。</param>
        protected void RaiseUpdated(string name)
        {
            this.RaisePropertyChanged(name);
        }


        public Task<IDialogSession> Step(IEnumerable<Type> stepsType, object context)
        {
            return DialogManager.Step(stepsType, context);
        }

        public Task<IDialogSession> Step(IEnumerable<IRoutableViewModel> steps, object context)
        {
            return DialogManager.Step(steps, context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3, TStep4>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(context);
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(object context)
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel
            where TStep8 : IRoutableViewModel
        {
            return DialogManager.Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(context);
        }

        public Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IRoutableViewModel
        {
            return DialogManager.Dialog<TViewModel>();
        }

        public Task<IDialogSession> Dialog(IRoutableViewModel vm)
        {
            return DialogManager.Dialog(vm);
        }

        public Task<bool> MessageBox(string title, string content)
        {
            return DialogManager.MessageBox(title, content);
        }

        public void View<TViewModel>() where TViewModel : IRoutableViewModel
        {
            ViewManager.View<TViewModel>();
        }

        public void View<TViewModel>(NavigationParameter @params) where TViewModel : IRoutableViewModel
        {
            ViewManager.View<TViewModel>(@params);
        }

        public void View(IRoutableViewModel vm)
        {
            ViewManager.View(vm);
        }

        public void View(Type vmType)
        {
            ViewManager.View(vmType);
        }

        public void View(IRoutableViewModel vm, NavigationParameter @params)
        {
            ViewManager.View(vm, @params);
        }

        public virtual string UrlPathSegment => string.Empty;

        public IScreen HostScreen
        {
            get
            {
                return _ScreenExpression.Value;
            }
        }

        protected IDialogManager DialogManager
        {
            get
            {
                return _DialogManagerExpression.Value;
            }
        }

        protected IViewManager ViewManager
        {
            get
            {
                return _ViewManagerExpression.Value;
            }
        }

        public IFullLogger Logger => ViewManager.Logger;

        public RoutingState Router => ViewManager.Router;
    }
}