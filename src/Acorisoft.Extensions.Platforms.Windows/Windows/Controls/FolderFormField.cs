using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class FolderFormField : FormField
    {
        public static readonly DependencyProperty FolderProperty = DependencyProperty.Register(
            "Folder", typeof(string), typeof(FolderFormField), new PropertyMetadata(default(string)));

        public string Folder
        {
            get { return (string) GetValue(FolderProperty); }
            set { SetValue(FolderProperty, value); }
        }
    }
}