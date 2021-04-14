using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.TileMap
{
    /// <summary>
    /// <see cref="BrushMode"/> 画刷模式枚举类型用于指示当前画刷的类型。
    /// </summary>
    public enum BrushMode : byte
    {
        /// <summary>
        /// 用于表示一个地形画刷
        /// </summary>
        Terrain     = 0b001,

        /// <summary>
        /// 用于表示一个地貌画刷
        /// </summary>
        Ground      = 0b010,

        /// <summary>
        /// 用于表示一个元素画刷
        /// </summary>
        Element     = 0b100
    }

    /// <summary>
    /// <see cref=""/> 表示连接处的内容占比。
    /// </summary>
    public enum FillMode
    {
        /// <summary>
        /// 小于四分之一
        /// </summary>
        Thin,
        /// <summary>
        /// 小于二分之一
        /// </summary>
        Half,
        /// <summary>
        /// 小于四分之三
        /// </summary>
        Extra,
        /// <summary>
        /// 大于四份之三
        /// </summary>
        Large
    }
}
