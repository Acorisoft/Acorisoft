using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
using ImTools;
using ReactiveUI;

namespace Acorisoft.Studio.Views
{
    public partial class StickyNoteGalleryView : SpaPage<StickyNoteGallery>
    {
        public StickyNoteGalleryView()
        {
            InitializeComponent();
            //
            // 
            SearchBox.WhenAnyValue(x => x.Text)
                .Throttle(TimeSpan.FromMilliseconds(300), RxApp.MainThreadScheduler)
                .Subscribe(async x =>
                {
                    if (ViewModel == null || string.IsNullOrEmpty(x))
                    {
                        return;
                    }
                    await ViewModel.SearchAsync(x);
                });
        }

        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //
            // 
            BindingOperations.EnableCollectionSynchronization(ViewModel.Collection,new object());
            Items.SetBinding(ItemsControl.ItemsSourceProperty,new Binding
            {
                Source = ViewModel,
                Path = new PropertyPath("Collection"),
                Mode = BindingMode.OneWay,
            });
            
        }
    }
}