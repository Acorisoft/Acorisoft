using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IUnitMapData"/>
    /// </summary>
    public interface IUnitMapData
    {
        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制横坐标。
        /// </summary>
        int UnitX { get; }

        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制纵坐标。
        /// </summary>
        int UnitY { get; }


        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制方向。
        /// </summary>
        Direction Direction { get; }
    }
}
