using System;
using System.Collections;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Dialogs;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public static class ViewAware
    {
        #region InteractiveView

        public static void SetQuickView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                Locator.Current.GetService<TViewModel>(),
                null,
                null,
                null);
        }

        public static void SetExtraView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                null,
                null,
                Locator.Current.GetService<TViewModel>());
        }

        public static void SetToolView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                Locator.Current.GetService<TViewModel>(),
                null,
                null);
        }

        public static void SetContextView<TViewModel>() where TViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                null,
                null,
                Locator.Current.GetService<TViewModel>(),
                null);
        }

        public static void SetInteractiveView<TQuickViewModel, TContextViewModel, TToolViewModel, TExtraViewModel>()
            where TQuickViewModel : QuickViewModelBase, IQuickViewModel
            where TContextViewModel : QuickViewModelBase, IQuickViewModel
            where TToolViewModel : QuickViewModelBase, IQuickViewModel
            where TExtraViewModel : QuickViewModelBase, IQuickViewModel
        {
            ServiceLocator.ViewService.NavigateTo(
                Locator.Current.GetService<TQuickViewModel>(),
                Locator.Current.GetService<TContextViewModel>(),
                Locator.Current.GetService<TToolViewModel>(),
                Locator.Current.GetService<TExtraViewModel>());
        }

        #endregion

        #region NavigateTo

        public static void NavigateTo<TViewModel>() where TViewModel : PageViewModelBase, IPageViewModel
        {
            var vm = Locator.Current.GetService<TViewModel>();
            ServiceLocator.ViewService.NavigateTo(vm);
        }

        public static void NavigateTo<TViewModel>(Hashtable parameter)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            var vm = Locator.Current.GetService<TViewModel>();

            if (vm == null)
            {
                return;
            }

            vm.Parameter(parameter);

            ServiceLocator.ViewService.NavigateTo(vm);
        }

        public static void NavigateTo<TViewModel>(object arg1) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9}
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11}
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11},
                {Arg12, arg12}
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11},
                {Arg12, arg12},
                {Arg13, arg13},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13,
            object arg14) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11},
                {Arg12, arg12},
                {Arg13, arg13},
                {Arg14, arg14},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13,
            object arg14, object arg15) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11},
                {Arg12, arg12},
                {Arg13, arg13},
                {Arg14, arg14},
                {Arg15, arg15},
            });
        }

        public static void NavigateTo<TViewModel>(object arg1, object arg2, object arg3, object arg4, object arg5,
            object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13,
            object arg14, object arg15, object arg16) where TViewModel : PageViewModelBase, IPageViewModel
        {
            NavigateTo<TViewModel>(new Hashtable
            {
                {Arg1, arg1},
                {Arg2, arg2},
                {Arg3, arg3},
                {Arg4, arg4},
                {Arg5, arg5},
                {Arg6, arg6},
                {Arg7, arg7},
                {Arg8, arg8},
                {Arg9, arg9},
                {Arg10, arg10},
                {Arg11, arg11},
                {Arg12, arg12},
                {Arg13, arg13},
                {Arg14, arg14},
                {Arg15, arg15},
                {Arg16, arg16},
            });
        }

        public static void NavigateTo<TViewModel>(params object[] parameters)
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            var hashtable = new Hashtable();
            var n = 0;
            foreach (var parameter in parameters)
            {
                hashtable.Add($"{Arg}{n:N}", parameter);
                n++;
            }

            NavigateTo<TViewModel>(hashtable);
        }

        #endregion


        private class ManualBusyState : IDisposable
        {
            public ManualBusyState(string description)
            {
                ServiceLocator.ViewService.ManualStartBusyState(description);
            }

            public void Dispose()
            {
                ServiceLocator.ViewService.ManualEndBusyState();
            }
        }

        public static Task<IDialogSession> ShowDialog<TViewModel>()
            where TViewModel : DialogViewModelBase, IDialogViewModel
        {
            var vm = Locator.Current.GetService<TViewModel>();
            return ServiceLocator.ViewService.ShowDialog(vm);
        }

        public static Task<bool?> AwaitDelete(string title, string subtitle)
        {
            var vm = Locator.Current.GetService<DeletePromptViewModel>();
            if (vm == null)
            {
                return Task.FromResult<bool?>(false);
            }

            vm.Subtitle = subtitle;
            vm.Title = title;
            return ServiceLocator.ViewService.ShowPrompt(vm);
        }

        public static Task<bool?> AwaitQueryWindowClose()
        {
            var vm = Locator.Current.GetService<CloseWindowPromptViewModel>();
            return ServiceLocator.ViewService.ShowPrompt(vm);
        }

        public static Task<bool?> AwaitQueryPageClose()
        {
            
            var vm = Locator.Current.GetService<ClosePagePromptViewModel>();
            return ServiceLocator.ViewService.ShowPrompt(vm);
        }

        public static void Toast(string message)
        {
            ServiceLocator.ViewService.Toast(message);
        }

        public static void Toast(Exception ex)
        {
            ServiceLocator.ViewService.Toast(ex.Message);
        }

        public static IDisposable ForceBusyState(string description)
        {
            return new ManualBusyState(description);
        }

        public const string Arg1 = "arg1";
        public const string Arg2 = "arg2";
        public const string Arg3 = "arg3";
        public const string Arg4 = "arg4";
        public const string Arg5 = "arg5";
        public const string Arg6 = "arg6";
        public const string Arg7 = "arg7";
        public const string Arg8 = "arg8";
        public const string Arg9 = "arg9";
        public const string Arg10 = "arg10";
        public const string Arg11 = "arg11";
        public const string Arg12 = "arg12";
        public const string Arg13 = "arg13";
        public const string Arg14 = "arg14";
        public const string Arg15 = "arg15";
        public const string Arg16 = "arg16";
        public const string Arg = "arg";
    }
}