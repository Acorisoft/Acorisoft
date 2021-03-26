using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="MapData"/>
    /// </summary>
    public abstract class MapData : IMapData
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BrushMode Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RefId { get; set; }

        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制画刷。
        /// </summary>
        public Brush Brush { get; protected set; }

        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制横坐标。
        /// </summary>
        public double X { get; protected set; }

        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制纵坐标。
        /// </summary>
        public double Y { get; protected set; }
    }
}
