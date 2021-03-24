using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <see cref="IBrushSetManager"/> 当需要管理多个数据集的时候就使用xxxManager作为后缀名。
    /// </remarks>
    public interface IBrushSetManager : IDataSetManager<BrushSet, BrushSetProperty>
    {
    }
}
