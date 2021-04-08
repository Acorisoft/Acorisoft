using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="IDataSetProperty"/> 接口用于表示一个数据集属性支持，为使用者提供基础的属性访问支持。
    /// </summary>
    public interface IDataSetProperty
    {
        /// <summary>
        /// 获取或设置当前数据集的名称。
        /// </summary>
        string Name { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的摘要。
        /// </summary>
        string Summary { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的作者。
        /// </summary>
        string Author { get; set; }


        /// <summary>
        /// 获取或设置当前数据集的封面。
        /// </summary>
        Resource Cover { get; set; }
    }
}
