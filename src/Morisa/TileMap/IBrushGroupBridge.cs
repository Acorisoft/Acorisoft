using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.TileMap
{
    public interface IBrushGroupBridge : IBindable
    {
        /// <summary>
        /// 获取或设置当前画刷分组的唯一标识符。
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 获取或设置当前画刷分组的父元素唯一标识符。
        /// </summary>
        Guid ParentId { get; set; }

        /// <summary>
        /// 获取或设置当前画刷分组的名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前画刷分组的画刷模式。该值必须与根画刷一致。
        /// </summary>
        BrushMode Mode { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值用于保护当前分组不被删除。
        /// </summary>
        bool IsLock { get; set; }
    }
}
