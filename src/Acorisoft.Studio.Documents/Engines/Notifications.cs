using System;
using Acorisoft.Studio.Documents.ProjectSystem;
using LiteDB;
using MediatR;

namespace Acorisoft.Studio.Documents.Engines
{
    public class DocumentOpenNotification : INotification, IRequest
    {
        public DocumentOpenNotification(CompositionSet compositionSet)
        {
            CompositionSet = compositionSet ?? throw new ArgumentNullException(nameof(compositionSet));
            MainDatabase = compositionSet.MainDatabase ?? throw new ArgumentNullException(nameof(MainDatabase));
        }
        public LiteDatabase MainDatabase { get; }
        public ICompositionSet CompositionSet { get; }
    }

    public class DocumentCloseNotification : INotification, IRequest
    {
    }

    public class DocumentSwitchNotification : INotification, IRequest
    {
        public LiteDatabase Database { get; internal set; }
    }
}