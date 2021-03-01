using Acorisoft.Morisa.Dialogs;
using ReactiveUI;
using Splat;
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

namespace Acorisoft.Morisa.Samples
{
    /// <summary>
    /// DialogSampleView.xaml 的交互逻辑
    /// </summary>
    public partial class DialogSampleView : ReactiveUserControl<DialogSampleViewModel>
    {
        public DialogSampleView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialogMgr = Locator.Current.GetService<IDialogService>();
            var vm = await dialogMgr.Dialog<InsertTextDialogViewModel>();
            var result = vm.GetResult<InsertTextDialogViewModel>();
        }
    }
}
