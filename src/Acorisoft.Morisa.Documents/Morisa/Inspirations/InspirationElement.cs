using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    public abstract class InspirationElement : MorisaObject, IInspirationElement
    {
        private protected TagCollection _tagCollection;

        public InspirationElement()
        {
            _tagCollection = new TagCollection(this);
        }

        public ITagCollection Tags => _tagCollection;
    }
}
