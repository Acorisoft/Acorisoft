using Acorisoft.Morisa.ViewModels;
using $rootnamespace$.ViewModels;
using Acorisoft.Pandora.Inputs;
using DryIoc;
using ReactiveUI;
using Splat;
using Splat.DryIoc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace $rootnamespace$.Views
{
    /// <summary>
    /// NotificationView.xaml 的交互逻辑
    /// </summary>
    public partial class $safeitemname$View : ReactiveUserControl<$safeitemname$ViewModel>
    {
        public $safeitemname$View()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            });
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}