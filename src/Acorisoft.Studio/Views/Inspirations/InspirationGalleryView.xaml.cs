using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
using ReactiveUI;

namespace Acorisoft.Studio.Views
{
    public partial class InspirationGalleryView : SpaPage<InspirationGalleryViewModel>
    {
        public InspirationGalleryView()
        {
            InitializeComponent();
            
        }

        protected async override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
            {
                return;
            }

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                    handler => SearchBox.TextCompleted += handler, handler => SearchBox.TextCompleted -= handler)
                .Subscribe(x =>
                {
                    ViewModel.SearchAsync(SearchBox.Text);
                });
            
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                    handler => SearchBox.TextClear += handler, handler => SearchBox.TextClear -= handler)
                .Subscribe(x =>
                {
                    ViewModel.ResetAsync();
                });
            
            //
            // 
            BindingOperations.EnableCollectionSynchronization(ViewModel.Collection, new object());
            this.OneWayBind(ViewModel, vm => vm.Collection, v => v.Items.ItemsSource);
        }
    }
}