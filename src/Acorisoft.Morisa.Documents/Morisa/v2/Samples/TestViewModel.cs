using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DynamicData;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Acorisoft.Morisa.v2.Samples
{
    public class TestData
    {

    }

    public class DataFilter<T>
    {
        public bool Filter(T data)
        {
            return true;
        }
    }

    public class TestDataFactory
    {
        private SourceList<TestData> _EditableCollection;
        private ReadOnlyObservableCollection<TestData> _BindableCollection;
        private Subject<Func<TestData,bool>> _FilterStream;
        private Subject<IComparer<TestData>> _SorterStream;
        private Subject<IPageRequest> _PagerStream;

        public TestDataFactory()
        {
            _EditableCollection.Connect()
                               .Filter(_FilterStream)
                               .Sort(_SorterStream)
                               .Page(_PagerStream)
                               .Bind(out _BindableCollection)
                               .SubscribeOn(ThreadPoolScheduler.Instance)
                               .Subscribe();

            _EditableCollection.Edit(x =>
            {
            });
        }


        //
        // Sort
        public IObserver<Func<TestData, bool>> Selector
        {
            get
            {
                return _FilterStream;
            }
        }
        public ReadOnlyObservableCollection<TestData> Collection { get; }
    }

    /// <summary>
    /// <see cref="TestViewModel"/> 这个视图模型用来向大家展示如何设计一个合理得视图模型。
    /// </summary>
    public class TestViewModel
    {
        private TestDataFactory _Factory;
        private IDisposableCollector _Disposable;

        public TestViewModel(IDisposableCollector disposable, TestDataFactory factory)
        {
            
        }

        public string SerachPattern { get; set; }
        public ReadOnlyObservableCollection<TestData> Collection => _Factory.Collection;
    }
}
