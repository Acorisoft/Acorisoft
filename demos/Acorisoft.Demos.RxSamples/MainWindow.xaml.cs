using System.Reactive.Linq;
using DynamicData;
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
    public class Group
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Name { get; set; }
    }

    public class GroupAdapter
    {
        private ReadOnlyObservableCollection<GroupAdapter> _c;
        public GroupAdapter(Node<Group,Guid> x)
        {
            Id = x.Key;
            GroupId = x.Item.GroupId;
            var loader = x.Children
                          .Connect()
                          .Transform(x => new GroupAdapter(x))
                          .Bind(out _c)
                          .DisposeMany()
                          .Subscribe(x =>
                          {

                          });
            Name = x.Item.Name;
        }
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public GroupAdapter Item { get; set; }
        public GroupAdapter Parent { get; set; }
        public ReadOnlyObservableCollection<GroupAdapter> Children => _c;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SourceCache<Group,Guid> _EditableCollection;
        private ReadOnlyObservableCollection<GroupAdapter> _BindableCollection;
        private ReadOnlyObservableCollection<Group> _FlatCollection;

        public MainWindow()
        {
            InitializeComponent();
            _EditableCollection = new SourceCache<Group, Guid>(x => x.Id);
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = DateTime.Now.ToString()
            };
            var group1 = new Group
            {
                Id = Guid.NewGuid(),
                GroupId = group.Id,
                Name = DateTime.Now.ToString()
            };

            Func<Node<Group,Guid>,bool> predicate = (Node<Group,Guid> x) => x.IsRoot;

            _EditableCollection.AddOrUpdate(group);
            _EditableCollection.AddOrUpdate(group1);
            _EditableCollection.Connect()
                               .TransformToTree(x => x.GroupId, Observable.Return(predicate))
                               .Transform(x => new GroupAdapter(x))
                               .Bind(out _BindableCollection)
                               .Subscribe(x =>
                               {

                               });
            _EditableCollection.Connect()
                               .Bind(out _FlatCollection)
                               .DisposeMany()
                               .Subscribe();
        }


        public ReadOnlyObservableCollection<GroupAdapter> Collection => _BindableCollection; 
        public ReadOnlyObservableCollection<Group> Flat => _FlatCollection;

        private void Insert(object sender, RoutedEventArgs e)
        {
            _EditableCollection.AddOrUpdate(new Group
            {
                Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                Name = DateTime.Now.ToString()
            });
        }
    }
}
