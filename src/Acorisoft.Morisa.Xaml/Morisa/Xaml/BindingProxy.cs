using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Acorisoft.Morisa.Xaml
{
    /// <summary>
    /// <see cref="BindingProxy"/> 表示一个绑定代理类型。用于为XAML提供间接绑定帮助。
    /// </summary>
    public sealed class BindingProxy : Freezable
    {
        /// <summary>
        /// 表示一个数据上下文的依赖属性。
        /// </summary>
        public static readonly DependencyProperty DataContextProperty;

        static BindingProxy()
        {
            DataContextProperty = DependencyProperty.Register("DataContext",
                                                              typeof(object),
                                                              typeof(BindingProxy),
                                                              new PropertyMetadata(null));
        }


        /// <summary>
        /// 创建实例。
        /// </summary>
        /// <returns>返回一个新的 <see cref="BindingProxy"/> 类型实例。</returns>
        protected override sealed Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }


        /// <summary>
        /// 获取或设置当前 <see cref="BindingProxy"/> 实例所代理传递的数据上下文。
        /// </summary>
        public object DataContext
        {
            get => GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }

        /// <summary>
        /// 返回一个表示当前实例状态的文本字符串。
        /// </summary>
        /// <returns>返回一个表示当前实例状态的文本字符串。</returns>
        public override sealed string ToString()
        {
            return @$"DataContext : {(DataContext == null? "null" : "not null")}";
        }
    }
}
