using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Subjects;
using Acorisoft.Extensions.ComponentModel;

namespace Acorisoft.Extensions.Windows.Services
{
    /// <summary>
    /// <see cref="MessageType"/> 表示推送的图标类型。
    /// </summary>
    public enum MessageType
    {
        Info,
        Success,
        Warning,
        Error,
    }
    
    //--------------------------------------------------------------------------------------------------------------
    //
    //  IToastService / IViewService Interfaces
    //
    //--------------------------------------------------------------------------------------------------------------

    #region IToastService / IViewService Interfaces

    

    /// <summary>
    /// <see cref="IToastService"/> 接口表示一个抽象的消息弹窗接口，用于为用户提供消息弹窗支持。
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="message">指定要通知的内容</param>
        void Toast(string message);
        
        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="type">指定弹出的消息类型。</param>
        /// <param name="message">指定要通知的内容</param>
        void Toast(MessageType type, string message);
        
        
        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="icon">指定弹出的消息图标。</param>
        /// <param name="message">指定要通知的内容</param>
        void Toast(object icon, string message);
        
        IObservable<IToastViewModel> ToastStream { get; }
    }


    public partial interface IViewService : IToastService
    {
    }
    
    #endregion
    
    
    
    
    
    
    
    

    //--------------------------------------------------------------------------------------------------------------
    //
    //  IToastService Implementations
    //
    //--------------------------------------------------------------------------------------------------------------
    partial class ViewService
    {
        private Subject<IToastViewModel> _toastStream;
        
        private void InitializeToastService()
        {
            _toastStream = new Subject<IToastViewModel>();
            _disposable.Add(_toastStream);
        }
        
        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="message">指定要通知的内容</param>
        public void Toast(string message)
        {
            Toast(MessageType.Info, message);
        }

        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="type">指定弹出的消息类型。</param>
        /// <param name="message">指定要通知的内容</param>
        public void Toast(MessageType type, string message)
        {
            var toast = new ToastViewModel
            {
                MessageType = type,
                Message = message
            };
            
            ToastIntern(toast);
        }


        /// <summary>
        /// 弹出指定消息的推送。
        /// </summary>
        /// <param name="icon">指定弹出的消息图标。</param>
        /// <param name="message">指定要通知的内容</param>
        public void Toast(object icon, string message)
        {
            var toast = new ToastViewModel
            {
                Icon = icon,
                Message = message
            };
            
            ToastIntern(toast);
        }

        protected internal void ToastIntern(IToastViewModel toast)
        {
            if (toast == null)
            {
                return;
            }
            
            if (!_toastStream.HasObservers)
            {
                //
                //
                Debug.WriteLine("No ToastHost Listen to toast queue");
            }
            else
            {
                _toastStream.OnNext(toast);
            }
        }

        public IObservable<IToastViewModel> ToastStream => _toastStream;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    

    
    //--------------------------------------------------------------------------------------------------------------
    //
    //  Infrastructure
    //
    //--------------------------------------------------------------------------------------------------------------
    #region Infrastructure

    /// <summary>
    /// <see cref="IToastViewModel"/> 表示一个消息视图模型。
    /// </summary>
    public interface IToastViewModel : INotifyPropertyChanged
    {
        
        MessageType MessageType { get; set; }
        object Icon { get; set; }
        string Message { get; set; }
        DateTime LastAccessBy { get; set; }
        TimeSpan Offset { get; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ToastViewModel : Bindable, IToastViewModel
    {
        public ToastViewModel()
        {
            Offset = TimeSpan.FromSeconds(3);
        }
        
        public MessageType MessageType { get; set; }
        public object Icon { get; set; }
        public string Message { get; set; }
        public DateTime LastAccessBy { get; set; }
        public TimeSpan Offset { get; set; }
    }

    #endregion
}