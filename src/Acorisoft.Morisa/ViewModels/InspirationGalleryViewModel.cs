using Acorisoft.Morisa.Collections;
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
using InspirationCollection = DynamicData.Binding.ObservableCollectionExtended<Acorisoft.Morisa.Inspirations.InspirationElement>;

namespace Acorisoft.Morisa.ViewModels
{
    [HomePage]
    public partial class InspirationGalleryViewModel : ViewModelBase, IDropTarget
    {
        private CollectionWrapper<InspirationElement,InspirationCollection> _Collection;

        public InspirationGalleryViewModel(IDialogService dialogSrv)
        {
            //_CollectionView = CollectionViewSource.GetDefaultView(Collection);
            //_CollectionView.Filter = OnElementFiltering;
            _Collection = new CollectionWrapper<InspirationElement, InspirationCollection>(new InspirationCollection
            {
            });

            //
            // 初始化过滤器
            InitializeFilterCollection();
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
        }

        public object SortBy { get; set; }

        public string Keyword {
            get => _Collection.Keyword;
            set => _Collection.Keyword = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public CollectionWrapper<InspirationElement,InspirationCollection> Collection => _Collection;
        public ICommand ElementInsertCommand { get; }
        public ICommand ElementRemoveCommand { get; }
        public ICommand ElementClearCommand { get; }

    }
}
