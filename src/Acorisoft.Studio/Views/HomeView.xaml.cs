using System.Windows.Controls;
using System.Windows.Input;
using Acorisoft.Extensions.Windows.Views;
using Acorisoft.Studio.ViewModels;

namespace Acorisoft.Studio.Views
{
    public partial class HomeView : SpaPage<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void HomeView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.IsOpenProject = !ViewModel.IsOpenProject;
        }
    }
}