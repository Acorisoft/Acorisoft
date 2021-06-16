using System;
using Acorisoft.Extensions.ComponentModel;
using MediatR;

namespace Acorisoft.Extensions.Windows.Services
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IViewService : IActivityService
    {
    }

    public interface IActivityService
    {
        /// <summary>
        /// 开启一个活动
        /// </summary>
        /// <param name="description">指定活动的主题。</param>
        void StartActivity(string description);

        /// <summary>
        /// 更新一个活动
        /// </summary>
        /// <param name="description">指定活动的主题。</param>
        void UpdateActivity(string description);

        /// <summary>
        /// 结束一个活动
        /// </summary>
        void EndActivity();
    }

    partial class ViewService : IViewService
    {
        /// <summary>
        /// 开启一个活动
        /// </summary>
        /// <param name="description">指定活动的主题。</param>
        public void StartActivity(string description)
        {
            _mediator.Publish(new StartActivityRequest(description));
        }

        /// <summary>
        /// 更新一个活动
        /// </summary>
        /// <param name="description">指定活动的主题。</param>
        public void UpdateActivity(string description)
        {
            _mediator.Publish(new UpdateActivityRequest(description));
        }

        /// <summary>
        /// 结束一个活动
        /// </summary>
        public void EndActivity()
        {
            _mediator.Publish(new EndActivityRequest());
        }
    }

    #region Requests

    public class StartActivityRequest : IStartActivityRequest
    {
        public StartActivityRequest(string description)
        {
            Description = description;
        }
        
        public string Description { get; }
    }
    
    public class EndActivityRequest : IEndActivityRequest
    {
        
    }
    
    public class UpdateActivityRequest : IUpdateActivityRequest
    {
        public UpdateActivityRequest(string description)
        {
            Description = description;
        }
        
        public string Description { get; }
    }
    public interface IStartActivityRequest : INotification
    {
        string Description { get; }
    }

    public interface IEndActivityRequest : INotification
    {
    }

    public interface IUpdateActivityRequest : INotification
    {
        string Description { get; }
    }
    
    #endregion

    public interface IActivityIndicator : INotificationHandler<IStartActivityRequest>, INotificationHandler<IEndActivityRequest>,
        INotificationHandler<IUpdateActivityRequest>
    {
    }

    /// <summary>
    /// <see cref="IActivityAmbient"/> 表示一个抽象的活动环境。用于为应用程序提供更新、关闭活动通知支持。
    /// </summary>
    public interface IActivityAmbient : IDisposable
    {
        /// <summary>
        /// 更新活动通知。
        /// </summary>
        /// <param name="description">指定要更新的通知。</param>
        void Update(string description);
    }

    public class ActivityAmbient : Disposable, IActivityAmbient
    {
        private readonly IViewService _service;
        
        public ActivityAmbient(IViewService service, string description)
        {
            _service = service;
            _service.StartActivity(description);
        }

        protected override void OnDisposeManagedCore()
        {
            _service?.EndActivity();
        }

        public void Update(string description)
        {
            _service?.UpdateActivity(description);
        }
    }
}