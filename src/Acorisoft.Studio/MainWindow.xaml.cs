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
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Studio.ViewModels;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MouseDoubleClick += OnMouseDoubleClick;
        }

        private async void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var service = (IViewService) ServiceProvider.GetService(typeof(IViewService)) ?? new ViewService();
            // await service.ShowDialog(new MockupDialogViewModel());
            service.Toast("Hello", null,TimeSpan.FromSeconds(1));
        }
    }
}