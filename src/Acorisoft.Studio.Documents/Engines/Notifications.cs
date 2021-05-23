using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    public class DocumentOpenNotification : INotification, IRequest
    {
    }

    public class DocumentCloseNotification : INotification, IRequest
    {
    }

    public class DocumentSwitchNotification : INotification, IRequest
    {
    }
}