using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="UnitMapData"/>
    /// </summary>
    public abstract class UnitMapData : MapData, IUnitMapData
    {
        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制横坐标。
        /// </summary>
        public int UnitX { get; protected set; }

        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制纵坐标。
        /// </summary>
        public int UnitY { get; protected set; }


        /// <summary>
        /// 获取当前 <see cref="UnitMapData"/> 的绘制方向。
        /// </summary>
        public Direction Direction { get; protected set; }
    }
}
