using System;
using System.Reactive;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows.Services
{
    
    //--------------------------------------------------------------------------------------------------------------
    //
    // IDialogService / IViewService Interfaces
    //
    //--------------------------------------------------------------------------------------------------------------
    public interface IDialogService
    {
        /// <summary>
        /// 对话框开启
        /// </summary>
        IObservable<Unit> DialogOpenStream { get; }
        
        /// <summary>
        /// 对话框关闭
        /// </summary>
        IObservable<Unit> DialogCloseStream { get; }
        
        /// <summary>
        /// 对话框更改
        /// </summary>
        IObservable<IDialogViewModel> DialogUpdateStream { get; }
    }
    
    public partial interface IViewService : IDialogService
    {
        
    }
    
    partial class ViewService
    {
        private Subject<Unit> _dialogOpen;
        private Subject<Unit> _dialogClose;
        private Subject<IDialogViewModel> _dialogChanged;
        
        private void InitializeDialogService()
        {
            _dialogOpen = new Subject<Unit>();
            _dialogChanged = new Subject<IDialogViewModel>();
            _dialogClose = new Subject<Unit>();
            
            _disposable.Add(_dialogChanged);
            _disposable.Add(_dialogClose);
            _disposable.Add(_dialogOpen);
        }

        /// <summary>
        /// 对话框开启
        /// </summary>
        public IObservable<Unit> DialogOpenStream => _dialogOpen;

        /// <summary>
        /// 对话框关闭
        /// </summary>
        public IObservable<Unit> DialogCloseStream => _dialogClose;

        /// <summary>
        /// 对话框更改
        /// </summary>
        public IObservable<IDialogViewModel> DialogUpdateStream => _dialogChanged;
    }
}