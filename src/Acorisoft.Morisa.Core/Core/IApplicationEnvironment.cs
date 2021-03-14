using System;
using System.Collections.Generic;

namespace Acorisoft.Morisa.Core
{
    public interface IApplicationEnvironment
    {
        List<ICompositionSetStore> Projects { get; set; }
        ICompositionSetStore CurrentProject { get; set; }
        string WorkingDirectory { get; set; }
    }
}
