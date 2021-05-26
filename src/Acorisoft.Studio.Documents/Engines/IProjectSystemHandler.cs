using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    public interface IProjectSystemHandler : INotificationHandler<CompositionSetOpenNotification>, INotificationHandler<CompositionSetCloseNotification>, INotificationHandler<CompositionSetSaveNotification>
    {
        
    }
}