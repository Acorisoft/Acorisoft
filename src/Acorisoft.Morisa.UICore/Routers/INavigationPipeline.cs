using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Routers
{
    public interface INavigationPipeline
    {
        void OnNext(NavigationFilterEventArgs e);
        IReadOnlyCollection<INavigationFilter> Filters { get; }
    }
}
