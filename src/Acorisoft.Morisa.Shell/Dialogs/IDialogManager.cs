using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        Task<IDialogSession> Step(IEnumerable<Type> steps);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        Task<IDialogSession> Step(IEnumerable<IRoutableViewModel> steps);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <typeparam name="TStep5"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <typeparam name="TStep5"></typeparam>
        /// <typeparam name="TStep6"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep4 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <typeparam name="TStep5"></typeparam>
        /// <typeparam name="TStep6"></typeparam>
        /// <typeparam name="TStep7"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <typeparam name="TStep5"></typeparam>
        /// <typeparam name="TStep6"></typeparam>
        /// <typeparam name="TStep7"></typeparam>
        /// <typeparam name="TStep8"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>()
            where TStep1 : IRoutableViewModel
            where TStep2 : IRoutableViewModel
            where TStep3 : IRoutableViewModel
            where TStep4 : IRoutableViewModel
            where TStep5 : IRoutableViewModel
            where TStep6 : IRoutableViewModel
            where TStep7 : IRoutableViewModel
            where TStep8 : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IRoutableViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<bool> MessageBox(string title, string content);
    }
}
