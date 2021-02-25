using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{
    public class TagCollection : ObservableCollection<ITag>, ITagCollection
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected MorisaObject Parent;

        public TagCollection(MorisaObject parent)
        {
            Parent = parent ?? throw new ArgumentNullException("parent was null");
        }
    }
}
