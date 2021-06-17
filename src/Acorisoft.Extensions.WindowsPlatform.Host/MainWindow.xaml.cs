using System;
using System.Collections.Generic;
using System.Linq;
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
using Acorisoft.Extensions.Windows;
using Acorisoft.Extensions.Windows.Services;

namespace Acorisoft.Extensions.WindowsPlatform.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ServiceLocator.ToastService.Toast(MessageType.Info, "Notify");
            ServiceLocator.ToastService.Toast(MessageType.Success, "Success");
            ServiceLocator.ToastService.Toast(MessageType.Warning, "Warning");
            ServiceLocator.ToastService.Toast(MessageType.Error, "Danger");
        }
    }
}