using MediatR;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface ICompositionSetFileManager : INotificationHandler<CompositionSetOpenNotification>, INotificationHandler<CompositionSetCloseNotification>
    {
        
    }
}