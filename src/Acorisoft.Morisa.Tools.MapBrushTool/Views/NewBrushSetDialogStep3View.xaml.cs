using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Tools.ViewModels;
using Acorisoft.Morisa.ViewModels;
using DryIoc;
using ReactiveUI;
using Splat;
using Splat.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Morisa.Tools.Views
{
    /// <summary>
    /// NotificationView.xaml 的交互逻辑
    /// </summary>
    public partial class NewBrushSetDialogStep3View : DialogView<NewBrushSetDialogStep3ViewModel>
    {
        public NewBrushSetDialogStep3View()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            });
        }
        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            var opendlg = new SaveFileDialog
            {
                Filter = "Morisa画刷|*.mbx"
            };

            if(opendlg.ShowDialog() == DialogResult.OK)
            {
                ViewModel.File = opendlg.FileName;
            }
        }
    }
}