using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public class ApplicationEnvironment : IApplicationEnvironment
    {
        public ApplicationEnvironment()
        {
            Projects = new List<ICompositionSetStore>();
        }

        public string WorkingDirectory { get; set; }
        public List<ICompositionSetStore> Projects { get; set; }
        public ICompositionSetStore CurrentProject { get; set; }
    }
}
