using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="ICompositionSetStore"/> 表示一个打开过的设定集。
    /// </summary>
    public interface ICompositionSetStore
    {
        string Name { get; set; }
        string Directory { get; set; }
        string FileName { get; set; }
    }
}
