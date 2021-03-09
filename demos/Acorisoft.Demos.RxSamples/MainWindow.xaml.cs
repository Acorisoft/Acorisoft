using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Reactive.Threading;
using DryIoc;
using System.Threading;
using DynamicData.Binding;
using System.Collections.ObjectModel;

namespace Acorisoft.Demos.RxSamples
{
    public class Model
    {
        public string Text { get; set; }
    }

    public class Model1 : ReactiveObject
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
    }

    public interface IService
    {

    }

    public class ServiceA : IService
    {

    }

    public class ServiceB : IService
    {

    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Model> Collection;
        private Container _container;

        public MainWindow()
        {
            InitializeComponent();

            //
            // it test subscribeOn method
            Debug.WriteLine($"main thread is : {Thread.CurrentThread.ManagedThreadId}");
            Model1 = new Model1();
            Model1.WhenAnyValue(x => x.Text)
                  .SubscribeOn(RxApp.TaskpoolScheduler)
                  .Subscribe(x => Debug.WriteLine($"work on thread:{Thread.CurrentThread.ManagedThreadId}"));
            Model1.Text = "A1";
            Model1.Text = "B1";

            //
            // it test can subscribeOn method work on thread pool scheduler
            Collection = new ObservableCollection<Model>();
            Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Collection, "CollectionChanged")
                      .ObserveOn(NewThreadScheduler.Default)
                      .Select(x => x!)
                      .Subscribe(x => Debug.WriteLine($"collection work on thread:{Thread.CurrentThread.ManagedThreadId}"));
            Collection.Add(new Model());

            

            //
            // it test can container return an enumerator for service
            _container = new Container();
            _container.Register<IService, ServiceA>();
            _container.Register<IService, ServiceB>();
            var services = _container.Resolve<IEnumerable<IService>>();
        }

        public Model Model { get; set; }
        public Model1 Model1 { get; set; }
    }
}
