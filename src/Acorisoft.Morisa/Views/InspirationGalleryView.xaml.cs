using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.ViewModels;
using ReactiveUI;
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

namespace Acorisoft.Morisa.Views
{
    /// <summary>
    /// InspirationGalleryView.xaml 的交互逻辑
    /// </summary>
    [ViewModel(typeof(InspirationGalleryViewModel))]
    public partial class InspirationGalleryView : ReactiveUserControl<InspirationGalleryViewModel>
    {
        private readonly IDialogService _dialogSrv;

        public InspirationGalleryView(IDialogService dialogService)
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            });

            _dialogSrv = dialogService;
        }

        private async void DoOpenInsertWizard(object sender, ExecutedRoutedEventArgs e)
        {
            var session = await _dialogSrv.Dialog<InspirationGalleryInsertWizardViewModel>();

            if(session.IsCompleted)
            {
                var result = session.GetResult<InspirationGalleryInsertWizardViewModel>();
                var insertSession = await session.NewSession(result.Insertion.Type);
                
            }
        }

        private void CanOpenInsertWizard(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
