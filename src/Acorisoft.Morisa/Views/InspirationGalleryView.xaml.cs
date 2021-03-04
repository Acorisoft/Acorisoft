using Acorisoft.Morisa.Collections;
using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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

namespace Acorisoft.Morisa.Views
{
    /// <summary>
    /// InspirationGalleryView.xaml 的交互逻辑
    /// </summary>
    [ViewModel(typeof(InspirationGalleryViewModel))]
    public partial class InspirationGalleryView : ReactiveUserControl<InspirationGalleryViewModel>
    {
        private readonly IDialogService _dialogSrv;
        private ICollectionView _CollectionView;

        public InspirationGalleryView(IDialogService dialogService)
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            });

            PART_SearchBox
                .WhenAnyValue(x => x.Text)
                .Throttle<string>(TimeSpan.FromMilliseconds(300),RxApp.MainThreadScheduler)
                .Where(x => !string.IsNullOrEmpty(x))
                .Subscribe(x =>
                {
                    ViewModel.Keyword = x;
                });

            _dialogSrv = dialogService;
        }

        private void CanFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter is ICollectionPredicator;
        }

        private void DoFilter(object sender, ExecutedRoutedEventArgs e)
        {
            var predicator = e.Parameter as ICollectionPredicator;
            predicator.Keyword = ViewModel.Keyword;
            ViewModel.Filter = predicator;
        }
    }
}
