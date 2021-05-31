namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public class CloseWindowPromptViewModel : DialogViewModelBase
    {
        public override bool VerifyAccess()
        {
            return true;
        }
    }
    
    public class ClosePagePromptViewModel : DialogViewModelBase
    {
        public override bool VerifyAccess()
        {
            return true;
        }
    }

    public class DeletePromptViewModel : DialogViewModelBase
    {
        public override bool VerifyAccess()
        {
            return true;
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}