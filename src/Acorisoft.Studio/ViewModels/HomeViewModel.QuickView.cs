using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeQuickViewModel : QuickViewModel
    {
        protected sealed override void OnStart()
        {
            Platform.InteractiveService.SetQuickView(this);
        }

        protected internal HomeViewModel HomeViewModel => (HomeViewModel)Parent;
    }
}