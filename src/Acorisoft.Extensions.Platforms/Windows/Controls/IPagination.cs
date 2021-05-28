namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public interface IPagination
    {
        /// <summary>
        /// 是否显示最后一页按钮。
        /// </summary>
        bool ShowLastButton { get; set; }
        
        /// <summary>
        /// 是否显示首页按钮
        /// </summary>
        bool ShowFirstButton { get; set; }
        
        /// <summary>
        /// 是否显示跳转页面按钮
        /// </summary>
        bool ShowGotoButton { get; set; }
        
        bool ShowNextPageButton { get; set; }
        bool ShowLastPageButton { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        int PageIndex { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        int PageCount { get; set; }
    }
}