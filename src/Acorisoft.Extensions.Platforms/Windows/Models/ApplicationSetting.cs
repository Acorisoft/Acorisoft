namespace Acorisoft.Extensions.Platforms.Windows.Models
{
    public class ApplicationSetting
    {
        /// <summary>
        /// 是否开启应用程序关闭提示。true 表示开启，false表示关闭。
        /// </summary>
        public bool EnableWindowCloseQuery { get; set; }
        
        /// <summary>
        /// 是否开启单例应用程序。true 表示开启，false表示关闭。
        /// </summary>
        public bool EnableSingleApplication { get; set; }
    }
}