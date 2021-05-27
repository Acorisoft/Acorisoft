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
using Acorisoft.Extensions.Platforms.Windows.Commands;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Studio.ProjectSystem;
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
            ViewAware.NavigateTo<HomeViewModel>();
            base.OnContentRendered(e);
        }

        private async void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var service = ServiceLocator.ViewService;
            var csm = ServiceLocator.CompositionSetManager;
            
            var session = await ViewAware.ShowDialog<NewProjectDialogViewModel>();
            if (session.IsCompleted && session.Result is INewProjectInfo projectInfo)
            {
                await csm.NewProject(projectInfo);
            }
        }
    }
}