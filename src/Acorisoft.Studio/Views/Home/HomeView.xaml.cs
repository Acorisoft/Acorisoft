using System.Windows;
using System.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
using ReactiveUI;

namespace Acorisoft.Studio.Views
{
    public partial class HomeView : SpaPage<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.OneWayBind(ViewModel, vm => vm.IsOpen, v => v.RootView.HasContentState);
        }
    }
}