using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    class MockupDocumentEngineA : DocumentEngine
    {
        public MockupDocumentEngineA(IDocumentEngineAquirement aquirement) : base(aquirement)
        {
        }

        protected override void OnDocumentSwitching(DocumentSwitchNotification notification)
        {
            Thread.Sleep(5000);
        }

        protected override void OnDocumentClosing(DocumentCloseNotification notification)
        {
            Thread.Sleep(3000);
        }
    }

    class MockupDocumentEngineB : DocumentEngine
    {
        public MockupDocumentEngineB(IDocumentEngineAquirement aquirement) : base(aquirement)
        {
        }

        protected override void OnDocumentSwitching(DocumentSwitchNotification notification)
        {
            Thread.Sleep(3000);
        }

        protected override void OnDocumentClosing(DocumentCloseNotification notification)
        {
            Thread.Sleep(3000);
        }
    }

    public abstract class DocumentEngine : INotificationHandler<DocumentCloseNotification>,
        INotificationHandler<DocumentSwitchNotification>
    {
        protected DocumentEngine(IDocumentEngineAquirement aquirement)
        {
            Aquirement = aquirement ?? throw new ArgumentNullException(nameof(aquirement));
        }

        protected abstract void OnDocumentSwitching(DocumentSwitchNotification notification);
        protected abstract void OnDocumentClosing(DocumentCloseNotification notification);

        public Task Handle(DocumentCloseNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Aquirement.Set();
                OnDocumentClosing(notification);
                Aquirement.Unset();
            }, cancellationToken);
        }

        public Task Handle(DocumentSwitchNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Aquirement.Set();
                OnDocumentSwitching(notification);
                Aquirement.Unset();
            }, cancellationToken);
        }

        protected IDocumentEngineAquirement Aquirement { get; }
    }
}