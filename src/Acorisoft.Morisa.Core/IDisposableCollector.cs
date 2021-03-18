using System;
using System.Reactive.Disposables;

namespace Acorisoft.Morisa
{
    public interface IDisposableCollector : IDisposable
    {
        CompositeDisposable Disposable { get; }
        void Collect(IDisposable disposable);
        void Uncollect(IDisposable disposable);
    }
}
