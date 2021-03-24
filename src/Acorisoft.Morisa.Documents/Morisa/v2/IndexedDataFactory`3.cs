using DynamicData;
using DynamicData.Binding;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using DryIoc;

namespace Acorisoft.Morisa.v2
{
    public abstract class IndexedDataFactory<TIndex, TDocument, TDestDocument> : DataFactory
    {
        private protected readonly BehaviorSubject<Func<TIndex,bool>>                   FilterStream;  // Filter
        private protected readonly BehaviorSubject<IComparer<TIndex>>                   SorterStream;  // Sorter
        private protected readonly SourceList<TIndex>                                   EditableIndexCollection;
        private protected readonly ReadOnlyObservableCollection<TIndex>                 BindableIndexCollection;
        private protected readonly SourceList<TDocument>                                EditableDocumentCollection;
        private protected readonly ReadOnlyObservableCollection<TDestDocument>          BindableDocumentCollection;
    }
}
