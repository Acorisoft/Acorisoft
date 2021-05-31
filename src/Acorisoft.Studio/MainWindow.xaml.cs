using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
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
        }
        
        protected sealed override void OnContentRendered(EventArgs e)
        {
            ViewAware.NavigateTo<StickyNoteGalleryViewModel>();
            base.OnContentRendered(e);
        }

        #region NewProject

        
        private void CanExecute_NewProjectCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private async void OnExecuted_NewProjectCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var service = ServiceLocator.ViewService;
            var csm = ServiceLocator.CompositionSetManager;
            
            var session = await ViewAware.ShowDialog<NewProjectDialogViewModel>();
            if (session.IsCompleted && session.Result is INewProjectInfo projectInfo)
            {
                await csm.NewProject(projectInfo);
            }
        }

        #endregion
        
        #region Save

        
        private void CanExecute_SaveCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DataContext is AppViewModel viewModel && viewModel.IsOpen;
            e.Handled = true;
        }

        private async void OnExecuted_SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            await ServiceLocator.CompositionSetManager.Save();
        }

        #endregion
    }
}