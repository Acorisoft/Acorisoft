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
            ServiceLocator.ViewService.StartActivity("Hello");
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                ServiceLocator.ViewService.EndActivity();
            });
        }
    }
}