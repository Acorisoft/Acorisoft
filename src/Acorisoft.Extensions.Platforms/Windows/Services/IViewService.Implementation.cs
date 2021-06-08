namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public partial class ViewService
    {
        public ViewService()
        {
            InitializeActivity();
            InitializeDialog();
            InitializeToast();
            InitializeViewAware();
        }
    }
}