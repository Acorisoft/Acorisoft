using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Acorisoft.Morisa.Extensions
{

#pragma warning disable IDE0034

    /// <summary>
    /// <see cref="VisualTreeExtensions"/> 表示一个是视觉树拓展类型。
    /// </summary>
    public static class VisualTreeExtensions
    {
        /// <summary>
        /// 获得指定依赖对象的父对象。
        /// </summary>
        /// <typeparam name="T">指定要获取的父对象类型。</typeparam>
        /// <param name="element">指定要获取父对象的依赖对象实例，要求不能为空。</param>
        /// <returns>如果指定类型的父对象存在则返回该父对象实例，否则返回该类型的默认类型。</returns>
        public static T GetParent<T>(this DependencyObject element) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(element);

            while(parent is not null)
            {
                if(parent is T genericParentInstance)
                {
                    return genericParentInstance;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return default(T);
        }

        /// <summary>
        /// 获得指定依赖对象的父对象。
        /// </summary>
        /// <typeparam name="T">指定要获取的父对象类型。</typeparam>
        /// <param name="element">指定要获取父对象的依赖对象实例，要求不能为空。</param>
        /// <param name="depth">指定获取的深度。</param>
        /// <returns>如果指定类型的父对象存在则返回该父对象实例，否则返回该类型的默认类型。</returns>
        public static T GetParent<T>(this DependencyObject element, int depth) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(element);

            while (parent is not null && depth > 0)
            {
                if (parent is T genericParentInstance)
                {
                    return genericParentInstance;
                }

                depth--;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return default(T);
        }
    }
}
