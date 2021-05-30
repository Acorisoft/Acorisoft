using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;

namespace Acorisoft.Studio.ProjectSystem
{
    public class CompositionSetRequestQueue : ICompositionSetRequestQueue
    {
        private readonly ConcurrentQueue<Unit> _queue;
        private readonly Subject<Unit> _requesting;
        private readonly Subject<Unit> _responding;

        public CompositionSetRequestQueue()
        {
            _queue = new ConcurrentQueue<Unit>();
            _requesting = new Subject<Unit>();
            _responding = new Subject<Unit>();
        }
        public void Set()
        {
            if (_queue.Count == 0)
            {
                _requesting.OnNext(Unit.Default);
            }
            _queue.Enqueue(Unit.Default);
        }

        public void Unset()
        {
            if (_queue.TryDequeue(out _) && _queue.Count == 0)
            {
                _responding.OnNext(Unit.Default);
            };
        }

        public IObservable<Unit> Requesting => _requesting;
        public IObservable<Unit> Responding => _responding;
    }
}