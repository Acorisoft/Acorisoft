using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataProperty"/> 类型表示一个数据属性。
    /// </summary>
    public abstract class DataProperty : IDataProperty
    {
        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 摘要。
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 获取或设置当前接口的<see cref="IDataProperty"/> 主题。
        /// </summary>
        public string Topic { get; set; }
    }
}
