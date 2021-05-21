using ReactiveUI;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class PageViewModel : ViewModelBase, IPageViewModel
    {
        protected override void OnStart()
        {
            //
            // 设置
            base.OnStart();
        }

        public virtual bool KeepAlive => true;
    }
}