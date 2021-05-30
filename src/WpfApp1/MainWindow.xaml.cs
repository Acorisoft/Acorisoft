using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
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
using Acorisoft.Extensions.Platforms.Windows;
using ReactiveUI;

namespace WpfApp1
{
    public static class WindowCommands
    {
        public static RoutedCommand Next { get; } = new RoutedUICommand("Next", "Next", typeof(SpaWindow));
        public static RoutedCommand Last { get; } = new RoutedUICommand("Last", "Last", typeof(SpaWindow));
        public static RoutedCommand Completed { get; } = new RoutedUICommand("Completed", "Completed", typeof(SpaWindow));
        public static RoutedCommand Ignore { get; } = new RoutedUICommand("Ignore", "Ignore", typeof(SpaWindow));
        public static RoutedCommand Cancel { get; } = new RoutedUICommand("Cancel", "Cancel", typeof(SpaWindow));
        public static RoutedCommand Skip { get; } = new RoutedUICommand("Skip", "Skip", typeof(SpaWindow));
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SpaWindow
    {
        private readonly ISubject<int> _reply;
        private readonly ISubject<int> _behavior;
        private readonly ISubject<int> _subject;
        private readonly ISubject<int> _async;
        
        public MainWindow()
        {
            _reply = new ReplaySubject<int>();
            _behavior = new BehaviorSubject<int>(0);
            _subject = new Subject<int>();
            _async = new AsyncSubject<int>();
            var observer = Observer.Create<int>(x => Debug.WriteLine(x));
            
            _reply.OnNext(12);
            _behavior.OnNext(13);
            _subject.OnNext(14);
            _async.OnNext(15);

            _reply.Subscribe(observer);
            _behavior.Subscribe(observer);
            _subject.Subscribe(observer);
            _async.Subscribe(observer);
            
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}