using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Acorisoft.Extensions.Windows.Controls;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Studio.ViewModels;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : InteractiveWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}