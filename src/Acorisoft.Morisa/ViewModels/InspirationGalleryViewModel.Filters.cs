using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acorisoft.Morisa.Inspirations;
using System.Threading.Tasks;
using DynamicData.Binding;

namespace Acorisoft.Morisa.ViewModels
{
    public class InspirationElementFilter<TClass, TInterface> : CollectionFilter
    {
        public InspirationElementFilter(string key)
        {
            Key = key;
        }

        public override sealed bool Predicate(object element)
        {
            return element is TClass || element is TInterface;
        }
    }

    public sealed class DefaultInspirationElementFilter : CollectionFilter
    {
        public DefaultInspirationElementFilter()
        {
            Key = "App.Text.Inspiration.Filter.Default";
        }
    }

    partial class InspirationGalleryViewModel
    {
        private protected ObservableCollectionExtended<CollectionFilter> _FilterCollection;

        private const string MusicElementKey = "App.Text.Inspiration.Filter.Music";
        private const string PictureElementKey = "App.Text.Inspiration.Filter.Picture";
        private const string TextElementKey = "App.Text.Inspiration.Filter.Text";


        protected void InitializeFilterCollection()
        {
            _FilterCollection = new ObservableCollectionExtended<CollectionFilter>
            {
                new DefaultInspirationElementFilter(),
                new InspirationElementFilter<InspirationMusicElement,IInspirationMusicElement>(MusicElementKey),
                new InspirationElementFilter<InspirationPictureElement,IInspirationPictureElement>(PictureElementKey),
                new InspirationElementFilter<InspirationTextElement,IInspirationTextElement>(TextElementKey),
            };
        }

        public ObservableCollectionExtended<CollectionFilter> FilterCollection => _FilterCollection;
    }
}
