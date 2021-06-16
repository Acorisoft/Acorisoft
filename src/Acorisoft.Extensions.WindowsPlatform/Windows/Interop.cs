using Acorisoft.Extensions.Windows.Services;
using Splat;

namespace Acorisoft.Extensions.Windows
{
    public static class Interop
    {
        /// <summary>
        /// 使用Using模式开启一个活动
        /// </summary>
        /// <param name="description">活动的主题。</param>
        /// <example>
        /// using(var ambient = Interop.StartActivity("ad"))
        /// {
        ///     
        /// }
        /// </example>
        /// <returns>返回一个用于更新活动主题的 <see cref="IActivityAmbient"/>实例。</returns>
        public static IActivityAmbient StartActivity(string description)
        {
            return new ActivityAmbient(ServiceLocator.ViewService, description);
        }
    }
}