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
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Studio.Documents.ProjectSystem;
using Acorisoft.Studio.ViewModels;

namespace Acorisoft.Studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SpaWindow
    {
        public MainWindow() : base()
        {
            InitializeComponent();
            this.MouseDoubleClick += OnMouseDoubleClick;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            // ServiceLocator.ViewService.NavigateTo(new HomeViewModel());
            base.OnContentRendered(e);
        }

        private async void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // var service = (IViewService) ServiceProvider.GetService(typeof(IViewService)) ?? new ViewService();
            // await service.ForceBusyState(new ObservableOperation(() => Thread.Sleep(3000), "Waiting"));
            // // await service.ShowDialog(new MockupDialogViewModel());
            // service.Toast("Hello", null,TimeSpan.FromSeconds(1));
            
            // var pm = (IProjectManager) ServiceProvider.GetService(typeof(IProjectManager));
            // pm.MockupOpen();
        }
    }
}