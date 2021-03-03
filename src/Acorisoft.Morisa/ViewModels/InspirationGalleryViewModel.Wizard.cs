using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reactive;
using System.Reactive.Concurrency;

namespace Acorisoft.Morisa.ViewModels
{
    public class InspirationGalleryInsertWizardViewModel : ViewModelBase
    {
        public InspirationGalleryInsertWizardViewModel()
        {
            Collection = new ObservableCollectionExtended<InspirationElementInsertion>
            {
                new InspirationElementInsertion
                {
                    Key = InspirationGalleryViewModelConstants.Insertion_Text
                },
                new InspirationElementInsertion
                {
                    Key = InspirationGalleryViewModelConstants.Insertion_Music
                },
                new InspirationElementInsertion
                {
                    Key = InspirationGalleryViewModelConstants.Insertion_Picture
                }
            };
        }
        public InspirationElementInsertion Insertion { get; set; }
        public ObservableCollectionExtended<InspirationElementInsertion> Collection { get; }
    }
}
