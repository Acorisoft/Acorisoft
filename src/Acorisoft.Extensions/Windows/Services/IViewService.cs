using System;
using System.Reactive.Disposables;
using MediatR;
using Disposable = Acorisoft.Extensions.ComponentModel.Disposable;

namespace Acorisoft.Extensions.Windows.Services
{
    public partial interface IViewService
    {
        
    }
    
    public partial class ViewService : Disposable, IViewService, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly CompositeDisposable _disposable;
        
        public ViewService(IMediator mediator)
        {
            _mediator = mediator;
            _disposable = new CompositeDisposable();
            InitializeToastService();
        }

        protected override void OnDisposeManagedCore()
        {
            if (!_disposable.IsDisposed)
            {
                _disposable.Dispose();
            }
        }
    }
}