using System.Windows.Input;

namespace Acorisoft.Studio.ViewModels
{
    public interface IGalleryViewModelPaginator
    {
        /// <summary>
        /// 第一页命令
        /// </summary>
        ICommand FirstPageCommand { get; }
        
        /// <summary>
        /// 最后一页命令
        /// </summary>
        ICommand LastPageCommand { get; }
        
        /// <summary>
        /// 上一页命令
        /// </summary>
        ICommand PreviousPageCommand { get; }
        
        /// <summary>
        /// 下一页命令
        /// </summary>
        ICommand NextPageCommand { get; }
        
        /// <summary>
        /// 跳转到指定页面命令
        /// </summary>
        ICommand GotoPageCommand { get; }
    }
}