using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataProperty"/> 接口用于表示一个抽象的数据属性接口。
    /// </summary>
    public interface IDataProperty
    {
        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 作者。
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 摘要。
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 主题。
        /// </summary>
        string Topic { get; set; }
    }
}
