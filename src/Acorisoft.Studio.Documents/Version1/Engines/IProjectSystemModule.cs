using MediatR;

namespace Acorisoft.Studio.Engines
{
    public interface IProjectSystemModule : INotificationHandler<CompositionSetOpenNotification>, INotificationHandler<CompositionSetCloseNotification>, INotificationHandler<CompositionSetSaveNotification>
    {
        
    }
}