using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface ICompositionSetFileManager : INotificationHandler<CompositionSetOpenNotification>, INotificationHandler<CompositionSetCloseNotification>
    {
        Task<Uri> UploadImage(string fileName);

        Task<Uri> UploadImage(Stream stream);
        Stream GetStream(Uri uri);
    }
}