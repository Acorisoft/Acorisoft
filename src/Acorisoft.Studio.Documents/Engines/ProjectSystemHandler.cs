using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents.ProjectSystem;
using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    public abstract class ProjectSystemHandler : IProjectSystemHandler
    {
        protected ProjectSystemHandler(ICompositionSetRequestQueue requestQueue)
        {
            RequestQueue = requestQueue;
        }

        private void HandleCompositionSetOpen(CompositionSetOpenNotification notification)
        {
            RequestQueue.Set();
            OnCompositionSetOpening(notification);
            RequestQueue.Unset();
        }
        
        private void HandleCompositionSetClose(CompositionSetCloseNotification notification)
        {
            RequestQueue.Set();
            OnCompositionSetClosing(notification);
            RequestQueue.Unset();
        }

        protected abstract void OnCompositionSetOpening(CompositionSetOpenNotification notification);
        protected abstract void OnCompositionSetClosing(CompositionSetCloseNotification notification);
        
        public Task Handle(CompositionSetOpenNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleCompositionSetOpen(notification), cancellationToken);
        }

        public Task Handle(CompositionSetCloseNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(()=>HandleCompositionSetClose(notification), cancellationToken);
        }
        protected ICompositionSetRequestQueue RequestQueue { get; }
    }
}