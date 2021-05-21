using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.Threadings;
using Acorisoft.Extensions.Windows.ViewModels;
using Splat;

namespace Acorisoft.Extensions.Windows
{
    public static class ViewAware
    {
        public static void NavigateTo<TViewModel>() where TViewModel : PageViewModel
        {
            Platform.ViewService.NavigateTo(Locator.Current.GetService<TViewModel>());
        }

        public static void Splash<TViewModel>() where TViewModel : SplashViewModel
        {
            Platform.ViewService.NavigateTo(Locator.Current.GetService<TViewModel>());
        }

        #region Waiting

        public static Task Waiting<TBackground>(ObservableOperation operation)
            where TBackground : SplashViewModel, ISplashViewModel
        {
            Splash<TBackground>();
            return Platform.ViewService.Waiting(operation.Operation, operation.Description);
        }
        
        public static Task Waiting<TBackground>(IEnumerable<ObservableOperation> operations)
            where TBackground : SplashViewModel, ISplashViewModel
        {
            Splash<TBackground>();
            var tuples = operations.Select(operation => new Tuple<Action, string>(operation.Operation, operation.Description)).ToList();
            return Platform.ViewService.Waiting(tuples);
        }

        #endregion
    }
}