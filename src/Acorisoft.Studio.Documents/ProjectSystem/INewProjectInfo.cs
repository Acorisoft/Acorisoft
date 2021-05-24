using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface INewProjectInfo : ICompositionSetProperty
    {
        string Path { get; }
    }
}
