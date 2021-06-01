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
using Acorisoft.Studio.Models;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.ProjectSystems;
using Acorisoft.Studio.ViewModels;
using ReactiveUI;

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
            ViewAware.NavigateTo<HomeViewModel>();
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
            var css = ServiceLocator.ComposeSetSystem;
            
            var session = await ViewAware.ShowDialog<NewProjectDialogViewModel>();
            if (!session.IsCompleted || session.Result is not INewItemInfo<IComposeSetProperty> projectInfo)
            {
                return;
            }
            await css.NewAsync(projectInfo);
                
            ViewModelLocator.AppViewModel.Setting.RecentProject = new ComposeProject
            {
                Path = projectInfo.Path,
                Name = projectInfo.Name,
                Album = projectInfo.Item.Album,
                    
            };
        }

        #endregion

        #region OpenProject

        private void CanExecute_OpenProjectCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private async void OnExecuted_OpenProjectCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var service = ServiceLocator.ViewService;
            var css = ServiceLocator.ComposeSetSystem;
            
            var session = await ViewAware.ShowDialog<OpenProjectDialogViewModel>();
            if (!session.IsCompleted || session.Result is not INewItemInfo<ComposeProject> projectInfo)
            {
                return;
            }
            
            await css.OpenAsync(projectInfo.Item);
            ViewModelLocator.AppViewModel.Setting.RecentProject = projectInfo.Item;
        }


        #endregion
        
        #region GotoFunction

        private void CanExecute_GotoFunctionCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter is StudioFunction;
            e.Handled = true;
        }

        private async void OnExecuted_GotoFunctionCommand(object sender, ExecutedRoutedEventArgs e)
        {
            // ReSharper disable once InconsistentNaming
            var targetVM = ( e.Parameter as StudioFunction)?.GotoFunction();
            if (targetVM != null)
            {
                ViewAware.NavigateTo(targetVM);
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