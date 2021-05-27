using System.Collections;

namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public class PageViewModelBase : ViewModelBase, IPageViewModel
    {
        void IViewModelParameter.Parameter(Hashtable hashtable)
        {
            OnParameterReceiving(hashtable);
        }

        protected virtual void OnParameterReceiving(Hashtable parameters)
        {
            
        }
        
        public virtual string Title { get; }
    }
}