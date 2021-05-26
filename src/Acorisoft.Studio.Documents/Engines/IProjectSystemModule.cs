using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    public interface IProjectSystemModule : INotificationHandler<CompositionSetOpenNotification>, INotificationHandler<CompositionSetCloseNotification>, INotificationHandler<CompositionSetSaveNotification>
    {
        
    }
}