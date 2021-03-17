using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Collection
{
    public interface IDataSelector<T>
    {
        bool Filter(T data);
        IComparer<T> Sort();
    }
}
