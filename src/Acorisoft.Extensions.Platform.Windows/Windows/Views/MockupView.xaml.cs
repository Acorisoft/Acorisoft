using System.Windows.Controls;
using Acorisoft.Extensions.Windows.ViewModels;
using ReactiveUI;

namespace Acorisoft.Extensions.Windows.Views
{
    public partial class MockupView : ReactiveUserControl<SplashViewModel>
    {
        public MockupView()
        {
            InitializeComponent();
            this.WhenActivated(d => d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext)));
        }
    }
}