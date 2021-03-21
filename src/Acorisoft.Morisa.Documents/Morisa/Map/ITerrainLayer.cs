using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public interface ITerrainLayer : IMapLayer, IEnumerable<ITerrainData>
    {        
        /// <summary>
        /// 获取或设置当前地图的宽度。
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// 获取或设置当前地图的高度
        /// </summary>
        int Height { get; set; }
    }
}
