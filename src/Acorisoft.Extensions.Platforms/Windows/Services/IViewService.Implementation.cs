namespace Acorisoft.Extensions.Platforms.Services
{
    public partial class ViewService
    {
        public ViewService()
        {
            InitializeBusyState();
            InitializeDialog();
            InitializeToast();
            InitializeViewAware();
        }
    }
}