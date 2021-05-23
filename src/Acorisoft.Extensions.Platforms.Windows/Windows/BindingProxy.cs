using System.Windows;

namespace Acorisoft.Extensions.Platforms.Windows
{
    /// <summary>
    /// <see cref="BindingProxy"/> 类型表示一个用于实现数据上下文代理的类型，为XAML提供绑定代理支持。
    /// </summary>
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(default(object)));

        public object Data
        {
            get => (object) GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}