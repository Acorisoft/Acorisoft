using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Windows;
using Splat;
using System;
using System.Collections.Generic;
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
using System.IO;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading;
using Notification = Acorisoft.Morisa.Dialogs.Notification;
using System.Reactive.Disposables;
using Acorisoft.Morisa.ViewModels;
using System.Diagnostics;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ShellWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(x => x.ViewModel)
                    .BindTo(this , x => x.DataContext)
                    .DisposeWith(d);

            });
        }

        protected override void OnDataContextChanged(object sender , DependencyPropertyChangedEventArgs e)
        {
            
        }

        protected override void OnLoaded(object sender , RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender , RoutedEventArgs e)
        {

        }
    }
}
