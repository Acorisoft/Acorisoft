using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="LandformData"/> 表示一个元素画刷绘制的数据。
    /// </summary>
    public class LandformData : UnitMapData, ILandformData
    {
        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="LandformData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        public LandformData(double x, double y, Brush brush) : this((int)(x / 40d), (int)(y / 40d), brush)
        {

        }

        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="LandformData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        public LandformData(int x, int y, Brush brush) : this(x, y, brush, Direction.None)
        {
        }

        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="LandformData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        /// <param name="direction">绘制的方向。</param>
        public LandformData(int x, int y, Brush brush, Direction direction)
        {
            UnitX = x;
            UnitY = y;
            X = x * 40d;
            Y = y * 40d;
            Direction = direction;
            Brush = brush ?? throw new ArgumentNullException(nameof(brush));
        }

        public override sealed string ToString()
        {
            return $"{{{UnitX},{UnitY}}},{Brush.Id}";
        }
    }
}
