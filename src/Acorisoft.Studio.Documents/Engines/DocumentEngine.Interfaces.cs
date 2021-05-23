using System;
using System.Reactive;

namespace Acorisoft.Studio.Documents.Engines
{
    public interface IDocumentEngineAquirement
    {
        void Set();
        void Unset();
        IObservable<Unit> ProjectOpenStarting { get; }
        IObservable<Unit> ProjectOpenEnding{ get; }
    }
    public interface IDocumentEngine
    {
    
    }
}