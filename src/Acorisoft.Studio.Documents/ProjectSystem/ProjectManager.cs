using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Platforms.ComponentModel;
using Acorisoft.Studio.Documents.Engines;
using DynamicData;
using MediatR;
using Unit = System.Reactive.Unit;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class ProjectManager : ObservableObject , IProjectManager
    {
        private protected readonly SourceList<CompositionProject> Source;


        #region IDocumentEngineAquirement

        private class RequestQueue : IDocumentEngineAquirement
        {
            private readonly Queue<Unit> _queue;
            private readonly Subject<Unit> _openStarting;
            private readonly Subject<Unit> _openEnding;

            public RequestQueue()
            {
                _queue = new Queue<Unit>();
                _openEnding = new Subject<Unit>();
                _openStarting = new Subject<Unit>();
            }

            public void Set()
            {
                _queue.Enqueue(Unit.Default);
                _openStarting.OnNext(Unit.Default);
            }

            public void Unset()
            {
                if (_queue.Count > 0)
                {
                    _queue.Dequeue();
                }
                
                if (_queue.Count == 0)
                {
                    //
                    // Raise Notification
                    _openEnding.OnNext(Unit.Default);
                }
            }

            public IObservable<Unit> ProjectOpenStarting => _openStarting;
            public IObservable<Unit> ProjectOpenEnding => _openEnding;
        }
        
        #endregion

        private readonly RequestQueue _queue;
        private readonly IMediator _mediator;
        
        public ProjectManager(IMediator mediator)
        {
            //
            // 这是从数据库中加载项目
            Source = new SourceList<CompositionProject>();
            // Source.Connect()
            //     .Filter(x => x != null)
            //     .Page();
            _queue = new RequestQueue();
            _mediator = mediator;
        }

        //
        // ProjectManager 
        internal IDocumentEngineAquirement Aquirement => _queue;

        public void MockupOpen()
        {
            _mediator.Publish(new DocumentSwitchNotification());
        }
        public void Dispose()
        {
        }
    }
}