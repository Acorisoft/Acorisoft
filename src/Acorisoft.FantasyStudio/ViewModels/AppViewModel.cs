using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Spa;
using Splat;

namespace Acorisoft.FantasyStudio.ViewModels
{
    public class AppViewModel : AppViewModelBase, IAppViewModel
    {
        public static T Get<T>() where T : IViewModel
        {
            return Locator.Current.GetService<T>();
        }

        #region GetDefaultxxxBar

        protected override sealed IViewModel GetDefaultCharmBar()
        {
            return Get<DefaultCharmBarViewModel>();
        }

        protected override sealed IViewModel GetDefaultNavigateBar()
        {
            return Get<DefaultNavigateBarViewModel>();
        }

        protected override sealed IViewModel GetDefaultToolBar()
        {
            return Get<DefaultToolBarViewModel>();
        }

        protected override sealed IViewModel GetDefaultViewBar()
        {
            return Get<DefaultViewBarViewModel>();
        }

        #endregion GetDefaultxxxBar

        protected override IPageViewModel GetStartupViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
