using Acorisoft.Morisa.Inspirations;
using Acorisoft.Morisa.Views;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    [HomePage]
    public class InspirationGalleryViewModel : ViewModelBase, IDropTarget
    {
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
        }


        public ObservableCollectionExtended<InspirationElement> Collection { get; }

    }
}
