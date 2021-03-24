using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
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
