using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface IMapBrushSetInformation : IProfile
    {
        /// <summary>
        /// 获取或设置当前画刷集的标签。
        /// </summary>
        List<string> Tags { get; }
    }
}
