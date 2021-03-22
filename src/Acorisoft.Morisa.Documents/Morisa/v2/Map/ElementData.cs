using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="DrawDirection"/> 表示一个画刷绘制的方向
    /// </summary>
    public enum DrawDirection
    {
        None,
        R90,
        R180,
        R270
    }

    /// <summary>
    /// <see cref="ElementData"/> 表示一个元素画刷绘制的数据。
    /// </summary>
    public class ElementData : MapData, IElementData
    {
        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="ElementData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        public ElementData(double x, double y, Brush brush) : this((int)(x / 40d), (int)(y / 40d), brush)
        {

        }

        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="ElementData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        public ElementData(int x, int y, Brush brush) : this(x, y, brush, 1, DrawDirection.None)
        {
        }

        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="ElementData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        /// <param name="size">绘制的大小。</param>
        public ElementData(int x, int y, Brush brush, int size) : this(x, y, brush, size, DrawDirection.None)
        {

        }

        /// <summary>
        /// 使用指定的参数初始化一个新的 <see cref="ElementData"/> 类型实例。
        /// </summary>
        /// <param name="x">绘制的横坐标。</param>
        /// <param name="y">绘制的纵坐标。</param>
        /// <param name="brush">绘制的画刷。</param>
        /// <param name="size">绘制的大小。</param>
        /// <param name="direction">绘制的方向。</param>
        public ElementData(int x, int y, Brush brush, int size, DrawDirection direction)
        {
            UnitX = x;
            UnitY = y;
            X = x * 40d;
            Y = y * 40d;
            Direction = direction;
            Size = size > 0 ? size : 1;
            Brush = brush ?? throw new ArgumentNullException(nameof(brush));
        }

        /// <summary>
        /// 获取当前 <see cref="ElementData"/> 的绘制横坐标。
        /// </summary>
        public int UnitX { get; }

        /// <summary>
        /// 获取当前 <see cref="ElementData"/> 的绘制纵坐标。
        /// </summary>
        public int UnitY { get; }

        /// <summary>
        /// 获取当前 <see cref="ElementData"/> 的绘制方向。
        /// </summary>
        public DrawDirection Direction { get; }

        /// <summary>
        /// 获取当前 <see cref="ElementData"/> 的绘制大小。
        /// </summary>
        public int Size { get; }
    }
}
