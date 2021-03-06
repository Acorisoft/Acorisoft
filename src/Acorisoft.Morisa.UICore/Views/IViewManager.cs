using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Views
{
    public interface IViewManager : IScreen
    {
        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        /// <typeparam name="TViewModel">指定当前视图管理器将要导航到的视图模型类型。</typeparam>
        void View<TViewModel>() where TViewModel : IRoutableViewModel;

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        /// <typeparam name="TViewModel">指定当前视图管理器将要导航到的视图模型类型。</typeparam>
        void View<TViewModel>(NavigationParameter @params) where TViewModel : IRoutableViewModel;

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        void View(IRoutableViewModel vm);

        /// <summary>
        /// 跳转到指定类型的视图。
        /// </summary>
        void View(IRoutableViewModel vm, NavigationParameter @params);
    }
}
