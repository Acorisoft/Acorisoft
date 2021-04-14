using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.TileMap
{
    /// <summary>
    /// <see cref="IBrushBridge"/>
    /// </summary>
    public interface IBrushBridge : ISelectable
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
        /// 获取当前 <see cref="IBrushBridge"/> 类型的创建时间。
        /// </summary>
        DateTime Creation { get; }

        /// <summary>
        /// 获取当前 <see cref="IBrushBridge"/> 类型所适配的数据源。
        /// </summary>
        IBrush Source { get; }
    }
}
