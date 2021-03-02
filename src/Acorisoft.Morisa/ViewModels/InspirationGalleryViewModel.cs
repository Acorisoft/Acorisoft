using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Inspirations;
using Acorisoft.Morisa.Views;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{
    [HomePage]
    public partial class InspirationGalleryViewModel : ViewModelBase, IDropTarget
    {
        private readonly ICollectionView _CollectionView;

        public InspirationGalleryViewModel(IDialogService dialogSrv)
        {
            //_CollectionView = CollectionViewSource.GetDefaultView(Collection);
            //_CollectionView.Filter = OnElementFiltering;

            //
            // 初始化过滤器
            InitializeFilterCollection();
        }

        protected virtual bool OnElementFiltering(object element)
        {
            return true;
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
        }

        public object SortBy { get; set; }
        public CollectionFilter Filter { get; set; }
        public ObservableCollectionExtended<InspirationElement> Collection { get; }
        public ICommand ElementInsertCommand { get; }
        public ICommand ElementRemoveCommand { get; }
        public ICommand ElementClearCommand { get; }

    }
}
