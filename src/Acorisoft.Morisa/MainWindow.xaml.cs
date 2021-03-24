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

        protected override async void OnDataContextChanged(object sender , DependencyPropertyChangedEventArgs e)
        {
            var vm = ViewModelLocator.AppViewModel;
            if (vm.IsFirstTime)
            {
                var session = await vm.DialogManager.Dialog<SelectProjectDirectoryViewModel>();

                if(session.IsCompleted && session.GetResult<string>() is string targetDirectory)
                {
                    vm.WorkingDirectory = targetDirectory;
                    vm.IsFirstTime = false;
                }

                session = await vm.DialogManager.Dialog<GenerateCompositionSetViewModel>();

                if (session.IsCompleted && session.GetResult<ICompositionSetInfo>() is ICompositionSetInfo info)
                {
                    info.Directory = Path.Combine(vm.WorkingDirectory, Factory.GenerateId());
                    info.FileName = Path.Combine(info.Directory, CompositionSet.MainDatabaseName);
                    vm.CompositionSetManager.Load(info);
                }
            }
        }

        protected override void OnLoaded(object sender , RoutedEventArgs e)
        {
            ViewModel = ViewModelLocator.AppViewModel;
        }
    }
}
