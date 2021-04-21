using Acorisoft.Spa;
using Acorisoft.Studio.ViewModels;
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

namespace Acorisoft.Studio
{
    public class TestViewModel : ViewModelBase
    {

    }

    public class DialogViewModel : ViewModelBase, IDialogViewModel
    {
        public object GetResult()
        {
            return this;
        }
    }

    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView : ReactiveUserControl<TestViewModel>
    {
        public TestView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var session = await ViewModel.Dialog<DialogViewModel>();
            if (session.IsCompleted)
            {
            }
        }
    }
}
