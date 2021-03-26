using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Tools.ViewModels;
using Acorisoft.Morisa.ViewModels;
using DryIoc;
using Microsoft.Win32;
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
    // XXXDialogViewFunction.cs
    // XXXDialogView.cs
    /// <summary>
    /// NotificationView.xaml 的交互逻辑
    /// </summary>
    public partial class NewBrushesDialogView : DialogView<NewBrushesDialogViewFunction>
    {
        public NewBrushesDialogView() : base()
        {
            InitializeComponent();
        }

        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            var opendlg = new FolderBrowserDialog();

            if(opendlg.ShowDialog() == DialogResult.OK)
            {
                ViewModel.Folder = opendlg.SelectedPath;
            }
        }
    }
}