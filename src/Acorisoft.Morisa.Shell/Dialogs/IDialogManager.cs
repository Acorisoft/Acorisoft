using Acorisoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Dialogs
{
    /// <summary>
    /// <see cref="IDialogManager"/> 接口用于表示一个抽象的对话框接口。
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// 弹出一个确认对话框，用于提示用户接受某个操作。
        /// </summary>
        /// <param name="title">本次对话框的标题。</param>
        /// <param name="content">本次对话框的内容。</param>
        /// <returns>返回一个可等待的任务，任务如果完成则返回当前用户是否接受此操作。</returns>
        Task<bool> Confirm(string title, string content);

        /// <summary>
        /// 弹出一个确认对话框，用于提示用户接受某个操作。
        /// </summary>
        /// <param name="title">本次对话框的标题。</param>
        /// <param name="content">本次对话框的内容。</param>
        /// <returns>返回一个可等待的任务，任务如果完成则返回当前用户是否接受此操作。</returns>
        Task<bool> Confirm(string title, object content);

        /// <summary>
        /// 弹出一个确认对话框，用于提示用户接受某个操作。
        /// </summary>
        /// <typeparam name="TViewModel">当前确认对话框的内容视图模型类型。</typeparam>
        /// <param name="title">本次对话框的标题。</param>
        /// <returns>返回一个可等待的任务，任务如果完成则返回当前用户是否接受此操作。</returns>
        Task<bool> Confirm<TViewModel>(string title) where TViewModel : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task Notification(string title, string content);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task Notification(string title, object content);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="title"></param>
        /// <returns></returns>
        Task Notification<TViewModel>(string title) where TViewModel : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<IDialogSession> Dialog(IViewModel vm);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStep1"></typeparam>
        /// <typeparam name="TStep2"></typeparam>
        /// <typeparam name="TStep3"></typeparam>
        /// <typeparam name="TStep4"></typeparam>
        /// <returns></returns>
        Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel;

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
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel;

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
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel;

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
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel
            where TStep7 : IViewModel;

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
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel
            where TStep7 : IViewModel
            where TStep8 : IViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepViewModels"></param>
        /// <returns></returns>
        Task<IDialogSession> Step(IEnumerable<IViewModel> stepViewModels);
    }
}
