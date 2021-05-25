using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeContextViewModel : QuickViewModelBase
    {
        protected override void OnStart(IPageViewModel currentViewModel)
        {
            Ancestor = currentViewModel;
            Parent = Ancestor as HomeViewModel;
        }
        
        public IPageViewModel Ancestor { get; private set; }
        public HomeViewModel Parent { get; private set; }


        public string Name
        {
            get => Parent.CompositionSet.Property.Name;
            set;
        }
        
        public string Summary
        {
            get;
            set;
        }
    }
}