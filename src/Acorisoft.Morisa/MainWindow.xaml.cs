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
            if (ViewModel is AppViewModel vm && vm.IsFirstTime)
            {
                var manager = vm.DialogManager;
                var session = await manager.Dialog<SelectProjectFolderViewModel>();
                var appVM = ViewModel as AppViewModel;

                if (session.IsCompleted && session.GetResult<string>() is string projectFolder)
                {
                    appVM.ProjectFolder = projectFolder;
                }

                session = await manager.Dialog<GenerateProjectViewModel>();

                if (session.IsCompleted && session.GetResult<IMorisaProjectTargetInfo>() is IMorisaProjectTargetInfo targetInfo)
                {
                    //
                    // 合成新的项目名
                    targetInfo.Directory = Path.Combine(appVM.ProjectFolder , Guid.NewGuid().ToString("N"));
                    targetInfo.FileName = Path.Combine(targetInfo.Directory , MorisaProjectManager.ProjectMainDatabaseName);

                    //
                    // 确保文件夹存在
                    if (!Directory.Exists(targetInfo.Directory))
                    {
                        Directory.CreateDirectory(targetInfo.Directory);
                    }

                    //
                    //
                    appVM.ProjectManager.LoadOrCreateProject(targetInfo);
                    appVM.IsFirstTime = false;
                }
            }
        }

        protected override void OnLoaded(object sender , RoutedEventArgs e)
        {
            ViewModel = Locator.Current.GetService<AppViewModel>();
        }

        private void Button_Click(object sender , RoutedEventArgs e)
        {
            var appVM = ViewModel as AppViewModel;
            var db = (appVM.CurrentProject as MorisaProject).Database;
            db.BeginTrans();
            db.FileStorage.Upload(Guid.NewGuid().ToString("N") , @"C:\Users\zhongxin013\Documents\HZSG\Assets\ico_512x512.ico");
            Debug.WriteLine(db.Commit());
        }
    }
}
