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
using Acorisoft.Morisa.Views;

namespace Acorisoft.Morisa.Dialogs
{
    /// <summary>
    /// MessageBoxView.xaml 的交互逻辑
    /// </summary>
    [ViewModel(typeof(MessageBox))]
    public partial class MessageBoxView : ReactiveUserControl<MessageBox>
    {
        public MessageBoxView()
        {
            InitializeComponent();
        }
    }
}
