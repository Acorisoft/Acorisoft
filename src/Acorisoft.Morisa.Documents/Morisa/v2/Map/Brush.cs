using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    /// <summary>
    /// 画刷的寻址模式。
    /// </summary>
    public enum BrushMode : byte
    {
        Default = 0b00,
        Custom = 0b01,
        Thumbnail = 0b10
    } 

    /// <summary>
    /// <see cref="Brush"/> 表示绘制用的画刷。
    /// </summary>
    public class Brush
    {
        /// <summary>
        /// 用于定位画刷的实际标识符。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用于定位画刷的位置
        /// </summary>
        public BrushMode Mode { get; set; }

        /// <summary>
        /// 用于定位画刷的特殊模式
        /// </summary>
        public int Identifier { get; set; }
    }
}
