using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    public interface INavigateParameter
    {
        object this[string key]
        {
            get;
        }
    }
}
