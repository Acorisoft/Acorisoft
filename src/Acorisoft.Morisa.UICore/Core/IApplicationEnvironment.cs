using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IApplicationEnvironment : IDisposable
    {
        IContainer Container { get; }
    }
}
