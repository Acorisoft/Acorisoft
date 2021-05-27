using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ProjectSystem;
using DynamicData;

namespace Acorisoft.Studio.Engines
{
    /// <summary>
    /// <see cref="DocumentGalleryEngine"/> 表示一个文档画廊引擎。用于为应用程序提供画廊功能支持。
    /// </summary>
    public abstract class DocumentGalleryEngine<TIndex, TIndexWrapper, TDocument> : ProjectSystemModule, IDocumentGalleryEngine<TIndex, TIndexWrapper, TDocument>, IDisposable
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper<TIndex>
        where TDocument : Document
    {
        private protected readonly SourceList<TIndex> EditableCollection;
        private protected readonly ReadOnlyObservableCollection<TIndexWrapper> BindableCollection;
        private protected readonly ISubject<Func<TIndexWrapper, bool>> FilterStream;
        private protected readonly ISubject<IComparer<TIndexWrapper>> SorterStream;
        private protected readonly CompositeDisposable Disposable;
        
        protected DocumentGalleryEngine(Func<TIndex,TIndexWrapper> transformer, ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            Disposable = new CompositeDisposable();
            SorterStream = new BehaviorSubject<IComparer<TIndexWrapper>>(Comparer<TIndexWrapper>.Default);
            FilterStream = new BehaviorSubject<Func<TIndexWrapper, bool>>(x => x != null);
            EditableCollection = new SourceList<TIndex>();
            var disposable1 = EditableCollection.Connect()
                .Transform(transformer)
                .Filter(FilterStream)
                .Sort(SorterStream)
                .Bind(out BindableCollection)
                .Subscribe();
            
            Disposable.Add(disposable1);
            Disposable.Add(EditableCollection);
            Disposable.Add((BehaviorSubject<Func<TIndexWrapper, bool>>)FilterStream);
            Disposable.Add((BehaviorSubject<IComparer<TIndexWrapper>>)SorterStream);
        }

        public void Dispose()
        {
            Disposable?.Dispose();
        }
        
        public ReadOnlyObservableCollection<TIndexWrapper> Collection => BindableCollection;
    }
}