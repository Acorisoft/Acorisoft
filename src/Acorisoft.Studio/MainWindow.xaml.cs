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



        #region WindowCommands


        private void OnDialogCancel(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.Cancel();
        }

        private void OnDialogNextOrComplete(object sender, ExecutedRoutedEventArgs e)
        {

            ServiceLocator.DialogService.NextOrComplete();
        }

        private void OnDialogIgnoreOrSkip(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.IgnoreOrSkip();
        }


        private void OnDialogLast(object sender, ExecutedRoutedEventArgs e)
        {
            ServiceLocator.DialogService.Last();
        }


        private void CanDialogCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanCancel();
            e.Handled = true;
        }

        private void CanDialogNextOrComplete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanNextOrComplete();
            e.Handled = true;
        }

        private void CanDialogIgnoreOrSkip(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanIgnoreOrSkip();
            e.Handled = true;
        }

        private void CanDialogLast(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ServiceLocator.DialogService.CanLast();
            e.Handled = true;
        }
        #endregion

        protected override void OnContentRendered(EventArgs e)
        {
            ServiceLocator.ViewService.NavigateTo(new HomeViewModel());
            base.OnContentRendered(e);
        }

        private async void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var service = (IViewService) ServiceProvider.GetService(typeof(IViewService)) ?? new ViewService();
            var session = await service.ShowDialog(new NewProjectDialogViewModel());
        }
    }
}