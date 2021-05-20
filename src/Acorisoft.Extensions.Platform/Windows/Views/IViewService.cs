using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    public interface IViewService
    {
        bool CanGoBack { get; }
        
        /// <summary>
        /// 设置显示的视图。
        /// </summary>
        /// <param name="vm">传递要显示的视图所关联的视图模型。</param>
        void NavigateTo(IPageViewModel vm);

        /// <summary>
        /// 当导航事件发生时触发该事件。
        /// </summary>
        event NavigateToViewEventHandler Navigating;
    }
}