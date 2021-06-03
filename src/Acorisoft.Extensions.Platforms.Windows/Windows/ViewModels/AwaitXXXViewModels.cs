namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public class AwaitCloseWindowViewModel : DialogViewModelBase
    {
        public sealed override bool VerifyAccess()
        {
            return true;
        }
    }
    
    public class AwaitClosePageViewModel : DialogViewModelBase
    {
        public sealed override bool VerifyAccess()
        {
            return true;
        }
    }

    public class AwaitDeleteViewModel : DialogViewModelBase
    {
        public sealed override bool VerifyAccess()
        {
            return true;
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}