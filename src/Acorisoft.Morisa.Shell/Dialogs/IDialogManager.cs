using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public interface IDialogManager
    {
        Task<IDialogSession> Step<TStep1, TStep2>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel;

        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel
            where TStep8 : IRoutableViewModel;

        Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IRoutableViewModel;
        Task<bool> MessageBox(string title, string content);
    }
}
