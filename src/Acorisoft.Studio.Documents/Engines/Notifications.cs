using LiteDB;
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
        public LiteDatabase Database { get; internal set; }
    }
}