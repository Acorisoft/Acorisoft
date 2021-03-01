using Acorisoft.Morisa.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    /// <summary>
    /// <see cref="IInspirationElement"/> 定义了一个灵感元素的基本功能。
    /// </summary>
    /// <remarks>
    /// <see cref="IInspirationElement"/> 灵感元素
    /// </remarks>
    public interface IInspirationElement
    {
        /// <summary>
        /// 获取或设置应用于当前 <see cref="IInspirationElement"/> 灵感元素的标签内容。
        /// </summary>
        ITagCollection Tags { get; }
        DateTime CreationTime { get; set; }
        DateTime LastModifiedBy { get; set; }
    }
}
