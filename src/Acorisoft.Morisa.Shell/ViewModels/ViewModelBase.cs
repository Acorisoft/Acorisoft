using Acorisoft.Dialogs;
using Acorisoft.Views;
using LiteDB;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.ViewModels
{
#pragma warning disable IDE0090

    /// <summary>
    /// <see cref="ViewModelBase"/>
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IViewModel
    {
        [NonSerialized]
        [BsonIgnore]
        private static readonly Lazy<IScreen> _LazyScreenInstance;

        [NonSerialized]
        [BsonIgnore]
        private static readonly Lazy<IDialogManager> _LazyDialogManager;

        static ViewModelBase()
        {
            _LazyScreenInstance = new Lazy<IScreen>(() => GetService<IScreen>());
            _LazyDialogManager = new Lazy<IDialogManager>(() => GetService<IDialogManager>());
        }

        protected virtual void OnTransferParameter(INavigateParameter parameter)
        {

        }

        void IParameterTransfer.Transfer(INavigateParameter parameter)
        {
            OnTransferParameter(parameter);
        }

        #region Property Notification

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

        #endregion Property Notification

        #region Service Locator Methods

        protected static T GetService<T>()
        {
            return Locator.Current.GetService<T>();
        }

        protected IDialogManager DialogManager => _LazyDialogManager.Value;

        #endregion Service Locator Methods


        #region Dialog

        public Task<bool> Confirm(string title, string content)
        {
            return DialogManager.Confirm(title, content);
        }

        public Task<bool> Confirm(string title, object content)
        {
            return DialogManager.Confirm(title, content);
        }

        public Task<bool> Confirm<TViewModel>(string title) where TViewModel : IViewModel
        {
            return DialogManager.Confirm<TViewModel>(title);
        }

        public Task Notification(string title, string content)
        {
            return DialogManager.Notification(title, content);
        }

        public Task Notification(string title, object content)
        {
            return DialogManager.Notification(title, content);
        }

        public Task Notification<TViewModel>(string title) where TViewModel : IViewModel
        {
            return DialogManager.Notification<TViewModel>(title);
        }

        public Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IViewModel
        {
            return DialogManager.Dialog<TViewModel>();
        }

        public Task<IDialogSession> Dialog(IViewModel vm)
        {
            return DialogManager.Dialog(vm);
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3, TStep4>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>();
        }

        public Task<IDialogSession> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>()
            where TContext : IViewModel
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
            where TStep8 : IStepViewModel
        {
            return DialogManager.Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>();
        }

        public Task<IDialogSession> Step<TContext>(IEnumerable<IStepViewModel> stepViewModels) where TContext : IViewModel
        {
            return DialogManager.Step<TContext>(stepViewModels);
        }

        public Task<IDialogSession> Step(IEnumerable<IStepViewModel> stepViewModels, IViewModel Context)
        {
            return DialogManager.Step(stepViewModels, Context);
        }


        #endregion Dialog

        /// <summary>
        /// 获取或设置当前视图模型的标题.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlPathSegment => Title;

        /// <summary>
        /// 
        /// </summary>
        public IScreen HostScreen
        {
            get
            {
                return _LazyScreenInstance.Value;
            }
        }
    }
}
