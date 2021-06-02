using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Acorisoft.Extensions.Platforms.Windows;
using ReactiveUI;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SpaWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    var imageSource = Clipboard.GetImage();
                    using var ms = Interop.CopyBitmapToStream(imageSource);
                    using var fs = new FileStream(@"D:\Test.jpg", FileMode.Create);
                    ms.CopyTo(fs);
                    
                }
            }
            catch(Exception ex)
            {
                ViewAware.Toast(ex.Message);
            }
        }
    }
}