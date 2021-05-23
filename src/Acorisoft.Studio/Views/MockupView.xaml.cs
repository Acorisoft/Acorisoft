using System.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
using ReactiveUI;

namespace Acorisoft.Studio.Views
{
    public partial class MockupView : DialogPage<MockupDialogViewModel>
    {
        public MockupView()
        {
            this.WhenActivated(d => d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext)));
        }
    }
}