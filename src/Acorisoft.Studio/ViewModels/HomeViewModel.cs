using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeViewModel : PageViewModel
    {
        private bool _isOpenProject;

        public bool IsOpenProject
        {
            get => _isOpenProject;
            set => Set(ref _isOpenProject, value);
        }
    }
}