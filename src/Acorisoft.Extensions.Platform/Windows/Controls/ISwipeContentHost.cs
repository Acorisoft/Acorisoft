namespace Acorisoft.Extensions.Windows.Controls
{
    public interface ISwipeContentHost
    {
        /// <summary>
        /// 获取或设置一个值，该值表示左侧内容栏是否下滑。
        /// </summary>
        bool IsLeftSideOpen { get; set; }
        
        /// <summary>
        /// 获取或设置一个值，该值表示左侧内容栏是否下滑。
        /// </summary>
        bool IsTopSideOpen { get; set; }
        
        /// <summary>
        /// 获取或设置一个值，该值表示左侧内容栏是否下滑。
        /// </summary>
        bool IsRightSideOpen { get; set; }
        
        /// <summary>
        /// 获取或设置一个值，该值表示左侧内容栏是否下滑。
        /// </summary>
        bool IsBottomSideOpen { get; set; }
        
        /// <summary>
        /// 获取或设置一个值，该值表示左侧内容栏是否保持。
        /// </summary>
        bool IsLeftSidePinning { get; set; }
        bool IsTopSidePinning { get; set; }
        bool IsRightSidePinning { get; set; }
        bool IsBottomSidePinning { get; set; }
        object LeftSide { get; set; }
        object TopSide { get; set; }
        object RightSide { get; set; }
        object BottomSide { get; set; }
    }
}