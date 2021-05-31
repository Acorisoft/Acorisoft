using System.Windows;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class TextFormField : FormField
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(TextFormField), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}