using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
using ImTools;
using ReactiveUI;

namespace Acorisoft.Studio.Views
{
    public partial class StickyNoteGalleryView : SpaPage<StickyNoteGalleryViewModel>
    {
        public StickyNoteGalleryView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            BindingOperations.EnableCollectionSynchronization(ViewModel.Collection,new object());
            Items.SetBinding(ItemsControl.ItemsSourceProperty,new Binding
            {
                Source = ViewModel,
                Path = new PropertyPath("Collection"),
                Mode = BindingMode.OneWay,
            });
            var view = CollectionViewSource.GetDefaultView(Items.ItemsSource);
        }
    }
}