using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.TileMap
{
    /// <summary>
    /// <see cref="IBrush"/> 表示一个抽象的画刷接口，用于表示一个抽象画刷。
    /// </summary>
    public interface IBrush
    {
        /// <summary>
        /// 获取或设置当前画刷的唯一标识符。
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 获取或设置当前画刷的父元素唯一标识符。
        /// </summary>
        Guid ParentId { get; set; }

        /// <summary>
        /// 获取当前画刷的左边界填充模式。
        /// </summary>
        public FillMode Left { get; set; }

        /// <summary>
        /// 获取当前画刷的右边界填充模式。
        /// </summary>
        public FillMode Right { get; set; }

        /// <summary>
        /// 获取当前画刷的上边界填充模式。
        /// </summary>
        public FillMode Top { get; set; }

        /// <summary>
        /// 获取当前画刷的下边界填充模式。
        /// </summary>
        public FillMode Bottom { get; set; }
    }
}
