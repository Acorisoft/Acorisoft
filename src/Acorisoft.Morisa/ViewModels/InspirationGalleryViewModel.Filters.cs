using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acorisoft.Morisa.Inspirations;
using System.Threading.Tasks;
using DynamicData.Binding;
using Acorisoft.Morisa.Collections;
using static Acorisoft.Morisa.ViewModels.InspirationGalleryViewModelConstants;

namespace Acorisoft.Morisa.ViewModels
{
    public class InspirationElementPredicator : CollectionPredicator
    {
        public override bool Predicate(object element)
        {
            return ContainsKeyword(element);
        }

        protected override sealed bool ContainsKeywordCore(object element)
        {
            return element is InspirationElement && ContainsKeywordCore(element as InspirationElement);
        }

        protected virtual bool ContainsKeywordCore(InspirationElement element)
        {
            if(element is IInspirationMusicElement)
            {
                return !string.IsNullOrEmpty(Keyword) && nameof(IInspirationMusicElement).Contains(Keyword);
            }
            else if(element is IInspirationPictureElement)
            {
                return !string.IsNullOrEmpty(Keyword) && nameof(IInspirationPictureElement).Contains(Keyword);
            }
            else if (element is IInspirationTextElement)
            {
                return !string.IsNullOrEmpty(Keyword) && nameof(IInspirationTextElement).Contains(Keyword);
            }
            return true;
        }
    }

    public class InspirationElementPredicator<TClass, TInterface> : InspirationElementPredicator
    {
        public InspirationElementPredicator(string key)
        {
            Key = key;
        }

        public override sealed bool Predicate(object element)
        {
            return (element is TClass || element is TInterface) && ContainsKeyword(element);
        }
    }

    partial class InspirationGalleryViewModel
    {
        private protected ObservableCollectionExtended<ICollectionPredicator> _FilterCollection;
        private static readonly InspirationElementPredicator Default = new DefaultInspirationElementFilter();

        private sealed class DefaultInspirationElementFilter : InspirationElementPredicator
        {
            public DefaultInspirationElementFilter()
            {
                Key = Filter_All;
            }

            public override sealed bool Predicate(object element)
            {
                return ContainsKeyword(element);
            }
        }

        protected void InitializeFilterCollection()
        {
            _FilterCollection = new ObservableCollectionExtended<ICollectionPredicator>
            {
                Default,
                new InspirationElementPredicator<InspirationMusicElement,IInspirationMusicElement>(Filter_Music),
                new InspirationElementPredicator<InspirationPictureElement,IInspirationPictureElement>(Filter_Picture),
                new InspirationElementPredicator<InspirationTextElement,IInspirationTextElement>(Filter_Text),
            };
            Filter = _FilterCollection[0];
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollectionPredicator Filter {
            get => _Collection.Predicator;
            set => _Collection.Predicator = value;
        }

        public ObservableCollectionExtended<ICollectionPredicator> FilterCollection => _FilterCollection;
    }
}
