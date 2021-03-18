using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IGenerateContext<T> where T : class
    {
        string Id { get; set; }
        string FileName { get; set; }
        string Directory { get; set; }
        T Context { get; set; }
    }
}
