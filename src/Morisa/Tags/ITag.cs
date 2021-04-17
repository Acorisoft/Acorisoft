using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{
    /// <summary>
    /// <see cref="ITag"/> 接口用于表示一个抽象的标签接口。
    /// </summary>
    public interface ITag
    {
        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的唯一标识符。
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例父级的唯一标识符。
        /// </summary>
        Guid ParentId { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的颜色。
        /// </summary>
        string Color { get; set; }
    }
}
