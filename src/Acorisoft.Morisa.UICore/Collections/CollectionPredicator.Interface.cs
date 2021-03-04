using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collections
{
    public interface ICollectionPredicator
    {
        bool Predicate(object element);
        string Key { get; set; }
        string Keyword { get; set; }
    }
}
