using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataSetProperty"/> 类型实例用于为数据集提供属性存储支持。
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class DataSetProperty : IDataSetProperty
    {
        /// <summary>
        /// 获取或设置当前数据集的名称。
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的摘要。
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的作者。
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的封面。
        /// </summary>
        public Resource Cover { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
