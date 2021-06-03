using System;
using System.Diagnostics;
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

        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
            {
                return;
            }

            //
            // 
            BindingOperations.EnableCollectionSynchronization(ViewModel.Collection, new object());
            this.OneWayBind(ViewModel, vm => vm.Collection, v => v.Items.ItemsSource);
        }
    }
}