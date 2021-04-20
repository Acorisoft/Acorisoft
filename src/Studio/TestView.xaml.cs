using Acorisoft.Spa;
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
    public class TestViewModel : ReactiveObject, IParameterViewModel
    {
        void IParameterViewModel.Parameter(IReadOnlyDictionary<string, object> parameters)
        {

        }

        public string UrlPathSegment { get; }

        public IScreen HostScreen { get; }
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
    }
}
