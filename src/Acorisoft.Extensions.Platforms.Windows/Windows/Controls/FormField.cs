using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class FormField : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(FormField), new PropertyMetadata(default(string)));

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}