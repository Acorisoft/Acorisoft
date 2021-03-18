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
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Acorisoft.Demos.RxSamples
{
    public class StringAdapter
    {
        public string Text { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISubject<IPageRequest> Paginator;
        private ISubject<Func<StringAdapter,bool>> Filter;
        private ISubject<IComparer<StringAdapter>> Sorter;
        private ISubject<Func<StringAdapter,DateTime>> Grouper;
        private SourceList<string> _EditableCollection;
        private ReadOnlyObservableCollection<StringAdapter> _BindableCollection;
        private int _Page = 1;
        private int _Temp;
        private ObservableAsPropertyHelper<int> _TempOAPH;

        public MainWindow()
        {
            InitializeComponent();
            Sorter = new BehaviorSubject<IComparer<StringAdapter>>(Comparer<StringAdapter>.Default);
            Filter = new BehaviorSubject<Func<StringAdapter, bool>>(x => true);
            Paginator = new BehaviorSubject<IPageRequest>(new PageRequest(1,12));
            _EditableCollection = new SourceList<string>();
            _EditableCollection.Connect()
                               .Transform(x => new StringAdapter { Text = x })
                               .Filter(Filter)
                               .Page(Paginator)
                               .Sort(SortExpressionComparer<StringAdapter>.Ascending(x => x.Text))
                               .Bind(out _BindableCollection)
                               .SubscribeOn(Dispatcher)
                               .Subscribe(x =>
                               {

                               });
            _EditableCollection.AddRange(CreateDataCore());
        }

        protected IEnumerable<string> CreateDataCore()
        {
            var list = new List<string>();
            for(int i = 0; i < 100; i++)
            {
                list.Add(i.ToString());
            }
            return list;
        }

        public ReadOnlyObservableCollection<StringAdapter> Collection => _BindableCollection;

        private void Last(object sender, RoutedEventArgs e)
        {
            Paginator.OnNext(new PageRequest(_Page > 1 ? --_Page : 1, 12));
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            Paginator.OnNext(new PageRequest(_Page <9 ? ++_Page : 8, 12));
        }


        private void Search(object sender, RoutedEventArgs e)
        {
            Filter.OnNext(x => x.Text.Length > 1);
        }

        public int Value {
            get
            {
                return _TempOAPH.Value;
            } 
        }
    }
}
