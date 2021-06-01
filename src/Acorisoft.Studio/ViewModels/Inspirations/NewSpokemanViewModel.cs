using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Studio.ViewModels
{
    public class NewSpokemanViewModel : DialogViewModelBase
    {
        private string _name;
        public override bool VerifyAccess()
        {
            return string.IsNullOrEmpty(_name);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
    }
}