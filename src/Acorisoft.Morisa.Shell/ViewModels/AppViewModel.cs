using Acorisoft.Properties;
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
    /// <see cref="AppViewModel"/>
    /// </summary>
    public abstract class AppViewModel : ReactiveObject, IAppViewModel
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

        //---------------------------------------------------------------------------------------
        //
        // Fields
        //
        //---------------------------------------------------------------------------------------
        [NonSerialized]
        [BsonIgnore]
        private readonly Lazy<IScreen>              _LazyScreenInstance;
        private readonly List<INavigatePipeline>    _Pipelines;

        [NonSerialized]
        [BsonIgnore]
        private string _Title;

        [NonSerialized]
        [BsonIgnore]
        private IViewModel _LastVM;

        [NonSerialized]
        [BsonIgnore]
        private IViewModel _CurrentVM;


        [NonSerialized]
        [BsonIgnore]
        private INavigateParameter _CurrentParams;


        [NonSerialized]
        [BsonIgnore]
        private INavigateParameter _LastParams;


        [NonSerialized]
        [BsonIgnore]
        private readonly RoutingState _Router;


        [NonSerialized]
        [BsonIgnore]
        private readonly IFullLogger  _Logger;


        //---------------------------------------------------------------------------------------
        //
        // Constructors
        //
        //---------------------------------------------------------------------------------------
        protected AppViewModel()
        {
            _LazyScreenInstance = new Lazy<IScreen>(() => GetService<IScreen>());
            _Logger = InitializeLogger(this);
            _Router = new RoutingState();
            _Pipelines = new List<INavigatePipeline>();
        }


        //---------------------------------------------------------------------------------------
        //
        // Methods
        //
        //---------------------------------------------------------------------------------------

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

        protected static IFullLogger InitializeLogger(object instance)
        {
            return Locator.Current
                          .GetService<ILogManager>()
                          .GetLogger(instance.GetType());
        }

        protected static T GetService<T>()
        {
            return Locator.Current.GetService<T>();
        }

        #endregion Service Locator Methods

        #region IViewManager Methods

        public void DisplayViewFor(IViewModel ViewModel)
        {
            _LastVM = _CurrentVM;
            _LastParams = _CurrentParams;
            _CurrentVM = ViewModel;
            _CurrentParams = null;
            _Title = ViewModel.Title;

            //
            // 更新改变
            RaiseUpdated(nameof(ViewModel));
            RaiseUpdated(nameof(Title));


            //
            // 记录日志
            _Logger.Info(string.Format(SR.AppViewModel_Navigating_Forward, ViewModel.GetType().Name));

            //
            // 导航到指定的页面
            _Router.Navigate.Execute(ViewModel);
        }

        public void DisplayViewFor(IViewModel ViewModel, INavigateParameter Parameter)
        {
            _LastVM = _CurrentVM;
            _LastParams = _CurrentParams;
            _CurrentVM = ViewModel;
            _CurrentParams = Parameter;
            _Title = ViewModel.Title;

            //
            // 更新改变
            RaiseUpdated(nameof(ViewModel));
            RaiseUpdated(nameof(Title));

            //
            // 传递参数。
            if (ViewModel is IParameterTransfer transfer && Parameter is not null)
            {
                transfer.Transfer(Parameter);
            }

            //
            // 记录日志
            _Logger.Info(string.Format(SR.AppViewModel_Navigating_Forward, ViewModel.GetType().Name));

            //
            // 导航到指定的页面
            _Router.Navigate.Execute(ViewModel);
        }

        public void DisplayViewFor<TViewModel>() where TViewModel : ViewModelBase, IViewModel
        {
            var ViewModelType = typeof(TViewModel);
            if (Locator.CurrentMutable.HasRegistration(ViewModelType))
            {
                DisplayViewFor((IViewModel)Locator.Current.GetService(ViewModelType), null);
            }
            else
            {
                _Logger.Info(string.Format(SR.AppViewModel_Navigate_ViewModel_NotRegister, typeof(TViewModel).Name));

            }
        }

        public void DisplayViewFor<TViewModel>(INavigateParameter PassingParameters) where TViewModel : ViewModelBase, IViewModel
        {
            var ViewModelType = typeof(TViewModel);

            //
            // 检测导航的目标视图参数是否为空
            if (PassingParameters == null)
            {
                _Logger.Info(SR.AppViewModel_Navigate_Parameter_Null);
            }

            if (Locator.CurrentMutable.HasRegistration(ViewModelType))
            {
                DisplayViewFor((IViewModel)Locator.Current.GetService(ViewModelType), PassingParameters);
            }
            else
            {
                _Logger.Info(string.Format(SR.AppViewModel_Navigate_ViewModel_NotRegister, typeof(TViewModel).Name));
            }
        }

        public void DisplayViewFor(Type ViewModelType)
        {
            //
            // 检测导航的目标视图模型是否为空
            if (ViewModelType == null)
            {
                _Logger.Info(string.Format(SR.AppViewModel_Navigate_ViewModel_Null, ViewModelType.Name));
            }

            if (ViewModelType.IsAssignableTo(typeof(IViewModel)) && Locator.CurrentMutable.HasRegistration(ViewModelType))
            {
                DisplayViewFor((IViewModel)Locator.Current.GetService(ViewModelType), null);
            }
            else
            {

            }
        }

        public void DisplayViewFor(Type ViewModelType, INavigateParameter PassingParameters)
        {
            //
            // 检测导航的目标视图模型是否为空
            if (ViewModelType == null)
            {
                _Logger.Info(string.Format(SR.AppViewModel_Navigate_ViewModel_Null, ViewModelType.Name));
            }

            //
            // 检测导航的目标视图参数是否为空
            if (PassingParameters == null)
            {
                _Logger.Info(SR.AppViewModel_Navigate_Parameter_Null);
                return;
            }

            if (ViewModelType.IsAssignableTo(typeof(IViewModel)) && Locator.CurrentMutable.HasRegistration(ViewModelType))
            {
                DisplayViewFor((IViewModel)Locator.Current.GetService(ViewModelType), PassingParameters);
            }
            else
            {

            }
        }


        #endregion IViewManager Methods

        void IParameterTransfer.Transfer(INavigateParameter parameter)
        {

        }

        //---------------------------------------------------------------------------------------
        //
        // Properties
        //
        //---------------------------------------------------------------------------------------

        /// <summary>
        /// 获取或设置当前视图模型的标题.
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

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

        /// <summary>
        /// 获取当前正在活跃的视图模型。
        /// </summary>
        public IViewModel ViewModel => _CurrentVM;

        /// <summary>
        /// 获取当前上一个的视图模型。
        /// </summary>
        public IViewModel LastViewModel => _LastVM;

        /// <summary>
        /// 
        /// </summary>
        public RoutingState Router => _Router;
    }
}
